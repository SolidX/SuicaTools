using System.Buffers.Binary;
using System.Collections.Immutable;
using Pcsc;
using Pcsc.Common;
using Solidus.SuicaTools.Data;
using Windows.Devices.SmartCards;

namespace Solidus.SuicaTools.CardReaderCLI
{
    public class SuicaCardReaderService : ICardReaderService
    {
        private readonly TransitContext _ctx;
        public SmartCardReader? Reader { get; private set; }
        private Felica.AccessHandler? FelicaHandler { get; set; }

        public SuicaCardReaderService(TransitContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<bool> InitializeCardReader()
        {
            //TODO: Move all these console logs to real logging
            Console.WriteLine("Searching for card reader...");

            var deviceInfo = await SmartCardReaderUtils.GetFirstSmartCardReaderInfo(SmartCardReaderKind.Nfc);
            if (deviceInfo != null)
            {
                Console.WriteLine($"NFC reader '{deviceInfo.Name}' detected...");
            }
            else
            {
                Console.WriteLine($"No advertised NFC reader found.");
                deviceInfo = await SmartCardReaderUtils.GetFirstSmartCardReaderInfo(SmartCardReaderKind.Any);
                Console.WriteLine("Trying for any available reader...");

                if (deviceInfo != null)
                    Console.WriteLine($"Generic reader '{deviceInfo.Name}' found...");
                else
                {
                    Console.Error.WriteLine("No comptabible card reader detected...");
                    return false;
                }
            }

            if (deviceInfo.IsEnabled)
            {
                Reader = await SmartCardReader.FromIdAsync(deviceInfo.Id);
                Reader.CardAdded += CardAddedHandler;
                Reader.CardRemoved += CardRemovedHandler;
                Console.WriteLine("Actively listening on card reader...");
                return true;
            }

            return false;
        }

        private void CardRemovedHandler(SmartCardReader reader, CardRemovedEventArgs args)
        {
            Console.WriteLine($"Card removed");
        }

        private async void CardAddedHandler(SmartCardReader reader, CardAddedEventArgs args)
        {
            var card = args.SmartCard;

            try
            {
                using (SmartCardConnection conn = await args.SmartCard.ConnectAsync())
                {
                    //Card Type Identification
                    var cardId = new IccDetection(card, conn);
                    await cardId.DetectCardTypeAync();

                    Console.WriteLine("Connected to card\r\nPC/SC device class: " + cardId.PcscDeviceClass.ToString());
                    Console.WriteLine("Card name: " + cardId.PcscCardName.ToString());
                    Console.WriteLine("ATR: " + BitConverter.ToString(cardId.Atr));

                    if (cardId.PcscDeviceClass == DeviceClass.StorageClass && cardId.PcscCardName == CardName.FeliCa)
                    {
                        //Felica card may contain Suica data
                        Console.WriteLine("Felica card detected");
                        var history = await ReadFelicaCardData(conn);

                        Console.WriteLine("Suica History loaded");
                        if (history != null)
                        {
                            foreach (var entry in history)
                                await PrintSuicaTransaction(history, entry);
                        }

                        FelicaHandler = null;
                    }
                    else
                    {
                        //Definitely no Suica data
                        Console.Error.WriteLine("Unsupported card type detected.");
                    }
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
        }

        private async Task<FelicaCardIdentity?> Poll()
        {
            var cmd = FelicaCommandBuilder.Polling();
            byte[] rawResponse = await FelicaHandler.TransparentExchangeAsync(cmd);

            //rawResponse format: length (1 byte), responsecode (1 byte), idm (8 bytes), pmm (8 bytes), reqdata (optional 2 bytes)
            if (rawResponse[0] != rawResponse.Length)
            {
                Console.Error.WriteLine("Only partial response received...");
                return null;
            }
            if (rawResponse[1] != FelicaCommandCode.Polling + 1)
            {
                Console.Error.WriteLine("Reponse received to incorrect command...");
                return null;
            }

            return new FelicaCardIdentity(rawResponse.Skip(2).Take(8).ToArray(), rawResponse.Skip(10).Take(8).ToArray());
        }

        private async Task<IEnumerable<ushort>> RequestSystemCodes(byte[] IDm)
        {
            var cmd = FelicaCommandBuilder.RequestSystemCode(IDm);
            byte[] rawResponse = await FelicaHandler.TransparentExchangeAsync(cmd);

            if (rawResponse[0] != rawResponse.Length)
            {
                Console.Error.WriteLine("Only partial response received...");
                return Enumerable.Empty<ushort>();
            }
            if (rawResponse[1] != FelicaCommandCode.RequestSystemCode + 1)
            {
                Console.Error.WriteLine("Reponse received to incorrect command...");
                return Enumerable.Empty<ushort>();
            }

            var systemMap = new HashSet<ushort>();
            for (var i = 11; i < rawResponse.Length; i += 2)
            {
                var code = new byte[2] { rawResponse[i + 1], rawResponse[i] };
                systemMap.Add(BinaryPrimitives.ReadUInt16LittleEndian(code));
            }

            return systemMap;
        }

        private async Task<bool> VerifySuicaHistoryService(byte[] IDm)
        {
            var cmd = FelicaCommandBuilder.RequestService(IDm, FelicaServiceCode.SuicaHistory);
            byte[] rawResponse = await FelicaHandler.TransparentExchangeAsync(cmd);

            if (rawResponse[0] != rawResponse.Length)
            {
                Console.Error.WriteLine("Only partial response received...");
                return false;
            }
            if (rawResponse[1] != FelicaCommandCode.RequestService + 1)
            {
                Console.Error.WriteLine("Reponse received to incorrect command...");
                return false;
            }

            var keyVersion = BinaryPrimitives.ReadUInt16LittleEndian(new ReadOnlySpan<byte>(rawResponse, 11, 2));
            if (keyVersion == 0xFFFF)
            {
                Console.Error.WriteLine("Suica History is not present.");
                return false;
            }

            return true;
        }

        private async Task<SuicaHistory> GetSuicaHistory(byte[] IDm)
        {
            var history = new SuicaHistory();
            var blankEntry = Enumerable.Repeat<byte>(0x0, 16).ToImmutableArray();

            for (ushort blockNum = 0; blockNum < 20; blockNum++)
            {
                var cmd = FelicaCommandBuilder.ReadWithoutEncryption(IDm, FelicaServiceCode.SuicaHistory, blockNum, false);
                byte[] rawResponse = await FelicaHandler.TransparentExchangeAsync(cmd);

                if (rawResponse[0] != rawResponse.Length)
                {
                    Console.Error.WriteLine("Only partial response received...");
                    return history;
                }
                if (rawResponse[1] != FelicaCommandCode.ReadWithoutEncryption + 1)
                {
                    Console.Error.WriteLine("Reponse received to incorrect command...");
                    return history;
                }

                var status1 = rawResponse[10];
                var status2 = rawResponse[11];

                if (status1 == 0x00)
                {
                    var blocks = (ushort)rawResponse[12];

                    for (var i = 13; i < rawResponse.Length; i += 16)
                    {
                        var data = rawResponse.Skip(i).Take(16).ToArray();
                        if (!data.SequenceEqual(blankEntry))
                        {
                            var logEntry = new SuicaLogEntry(data, null); //TODO: Add context
                            history.Add(logEntry);
                        }
                    }
                }
                else
                {
                    //TODO: Error handling based on status flags mappings in 4.5.2 (see pg 112)
                    Console.Error.WriteLine($"An error occurred reading Block {blockNum} from the Service {FelicaServiceCode.SuicaHistory:X}.");
                }
            }

            return history;
        }

        private async Task<SuicaHistory?> ReadFelicaCardData(SmartCardConnection conn)
        {
            FelicaHandler = new Felica.AccessHandler(conn);

            var cardIdentity = await Poll();
            if (cardIdentity == null)
            {
                Console.WriteLine("Could not identify card.");
                return null;
            }

            var systemMap = await RequestSystemCodes(cardIdentity.IDm);

            //Check if this card contains a Saibane system on it.
            if (!systemMap.Contains(FelicaSystemCode.Saibane))
            {
                Console.WriteLine("This is not a Suica compatible card.");
                return null;
            }

            //Check if card has suica history service available
            if (await VerifySuicaHistoryService(cardIdentity.IDm))
            {
                //Get History
                return await GetSuicaHistory(cardIdentity.IDm);
                throw new NotImplementedException();
            }
            
            return null;
        }

        private async Task PrintSuicaTransaction(SuicaHistory h, SuicaLogEntry e)
        {
            var entry = await e.GetEntryStation();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Transaction No. {e.TransactionNumber} - {e.TransactionType}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"Date: {e.TransactionDate} \t ");
            if (e.IsASaleOfGoods()) Console.Write($"Time: {e.TransactionTime} \t POS ID: {e.PointOfSaleId}");
            Console.WriteLine();

            Console.Write($"Transaction Amount: ");
            Console.ForegroundColor = e.TransactionType == TransactionType.Charge ? ConsoleColor.Green : ConsoleColor.Red;
            Console.Write($"\u00A5{h.GetTransactionAmount(e.TransactionNumber)?.ToString() ?? "???"}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($" \t Balance: \u00A5{e.Balance}");

            if (e.TransactionType == TransactionType.FarePayment)
            {
                var exit = await e.GetExitStation();

                if (entry != null) Console.WriteLine($"Entry Station: {entry.LocalizedStationName ?? entry.NativeStationName} ({entry.LocalizedLineName ?? entry.NativeStationName})");
                if (exit != null) Console.WriteLine($"Exit Station:  {exit.LocalizedStationName ?? exit.NativeStationName} ({exit.LocalizedLineName ?? exit.NativeStationName})");
            }
            if (e.TransactionType == TransactionType.Charge)
            {
                if (entry != null) Console.WriteLine($"Reloaded At: {entry.LocalizedStationName ?? entry.NativeStationName}");
            }
            if (e.IsBusRelated())
            {
                var busStop = e.GetBusStop();
                if (busStop != null) Console.WriteLine($"Bus Line: {busStop.NativeStationName} ({busStop.NativeLineName})");
            }
            Console.WriteLine();
        }
    }
}

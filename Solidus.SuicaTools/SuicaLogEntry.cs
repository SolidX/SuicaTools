using Microsoft.EntityFrameworkCore;
using Solidus.SuicaTools.Data;

namespace Solidus.SuicaTools
{
    /// <summary>
    /// Represents a record in the Suica Log.
    /// </summary>
    /// <remarks>Found at Service 0x090F on the IC card.</remarks>
    /// <see href="https://osdn.net/projects/felicalib/wiki/suica" />
    public class SuicaLogEntry
    {
        private readonly TransitContext _context;

        public byte[] RawData { get; private set; }

        /// <summary>
        /// The kind of device the proccesed this transaction
        /// </summary>
        /// <remarks>Byte 0</remarks>
        public TerminalType TerminalType
        {
            get
            {
                return Enum.IsDefined(typeof(TerminalType), (TerminalType)RawData[0]) ? (TerminalType)RawData[0] : TerminalType.Unknown;
            }
        }

        /// <summary>
        /// Type of Transaction
        /// </summary>
        /// <remarks>Byte 1</remarks>
        public TransactionType TransactionType
        {
            get
            {
                return Enum.IsDefined(typeof(TransactionType), (TransactionType)RawData[1]) ? (TransactionType)RawData[1] : TransactionType.Unknown;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Byte 2</remarks>
        /// <see href="https://blog.kalan.dev/core-nfc-en/#felica-reader"/>
        public PaymentType PaymentMethod
        {
            get
            {
                return Enum.IsDefined(typeof(PaymentType), (PaymentType)RawData[2]) ? (PaymentType)RawData[2] : PaymentType.Unknown;
            }
        }

        /// <summary>
        /// Entry/Exit
        /// </summary>
        /// <remarks>Byte 3</remarks>
        /// <see href="https://blog.kalan.dev/core-nfc-en/#felica-reader"/>
        /// <see href="http://jennychan.web.fc2.com/format/suica.html"/>
        public EntryExitType EntryExitType
        {
            get
            {
                return Enum.IsDefined(typeof(EntryExitType), (EntryExitType)RawData[3]) ? (EntryExitType)RawData[3] : EntryExitType.Unknown;
            }
        }

        /// <summary>
        /// The date (JST) that this transaction occured.
        /// </summary>
        /// <remarks>Bytes 4 & 5</remarks>
        public DateOnly TransactionDate
        {
            //7 bits from the beginning is the year, 4 bits is the month, and the remaining 5 bits are the day
            get
            {
                var rawBits = BitConverter.ToUInt16(new byte[] { RawData[5], RawData[4] });
                var year = ((rawBits & 0b1111111000000000) >> 9) + 2000;
                var month = (rawBits & 0b0000000111100000) >> 5;
                var day = (rawBits & 0b0000000000011111);

                return new DateOnly(year, month, day);
            }            
        }

        #region Train-related members
        /// <summary>
        /// A identifier specifying the train line you entered the system from.
        /// </summary>
        /// <remarks>Byte 6</remarks>
        public byte? EntryLineCode
        {
            get
            {
                if (IsASaleOfGoods() || IsBusRelated())
                    return null;
                else
                    return RawData[6];
            }
        }
        
        /// <summary>
        /// An identifier spcificying the station you entered the system at.
        /// </summary>
        /// <remarks>Byte 7</remarks>
        public byte? EntryStationCode
        {
            get
            {
                if (IsASaleOfGoods() || IsBusRelated())
                    return null;
                else
                    return RawData[7];
            }            
        }
        /// <summary>
        /// A identifier specifying the train line you exited the system at.
        /// </summary>
        /// <remarks>Byte 8</remarks>
        public byte? ExitLineCode
        {
            get
            {
                if (IsASaleOfGoods() || IsBusRelated())
                    return null;
                else
                    return RawData[8];
            }
        }

        /// <summary>
        /// An identifier spcificying the station you exited from.
        /// </summary>
        /// <remarks>Byte 9</remarks>
        public byte? ExitStationCode
        {
            get
            {
                if (IsASaleOfGoods() || IsBusRelated())
                    return null;
                else
                    return RawData[9];
            }            
        }
        #endregion

        #region Bus & Tram related members
        /// <summary>
        /// An identifier for a particular transit operator
        /// </summary>
        /// <remarks>Bytes 6 & 7</remarks>
        public ushort? BusOperatorCode
        {
            get
            {
                if (IsBusRelated())
                {
                    return BitConverter.ToUInt16(new byte[] { RawData[7], RawData[6] });
                }
                return null;
            }
        }

        /// <summary>
        /// An identifier for a particular Bus Stop 
        /// </summary>
        /// <remarks>Bytes 8 & 9</remarks>
        public ushort? BusStopCode
        {
            get
            {
                if (IsBusRelated())
                {
                    return BitConverter.ToUInt16(new byte[] { RawData[9], RawData[8] });
                }
                return null;
            }
        }
        #endregion

        #region Sale of Goods related members
        /// <summary>
        /// The time (JST) this transaction occurred if it was a Sale of Goods, null otherwise.
        /// </summary>
        /// <remarks>Bytes 6 & 7</remarks>
        public TimeOnly? TransactionTime
        {
            get
            {
                if (IsASaleOfGoods())
                {
                    //The first 5 bits are hours, the 6 bits are minutes, and the last 5 bits are the number of seconds / 2
                    //(that's right the second portion of a timestamp is only approximate)

                    var rawBits = BitConverter.ToUInt16(new byte[] { RawData[7], RawData[6] });
                    var hours = (rawBits & 0b1111100000000000) >> 11;
                    var mins = (rawBits & 0b0000011111100000) >> 5;
                    var secs = (rawBits & 0b0000000000011111) * 2;

                    return new TimeOnly(hours, mins, secs);
                }
                return null;
            }
        }

        /// <summary>
        /// An identifier for the point of sale at which this purchase was made.
        /// </summary>
        public uint? PointOfSaleId
        {
            get
            {
                if (IsASaleOfGoods())
                {
                    return BitConverter.ToUInt16(new byte[] { RawData[9], RawData[8] });
                }
                return null;
            }            
        }
        #endregion

        /// <summary>
        /// The card balance after this transaction has been performed
        /// </summary>
        /// <remarks>
        /// Bytes 10 & 11
        /// Warning: I don't know if the balance can ever be negative so I've made an assumption
        /// Note: You need to be aware of the transaction that precedes this one in order to determine the value of this one
        /// </remarks>
        public uint Balance => BitConverter.ToUInt16(new byte[] { RawData[10], RawData[11] });
        
        /// <summary>
        /// An id number for this transaction.
        /// It seems there's a theoretical maximum of 16777215 transactions before it wraps back around to 0.
        /// </summary>
        /// <remarks>Bytes 12 through 14</remarks>
        public uint TransactionNumber => BitConverter.ToUInt32(new byte[] { RawData[14], RawData[13], RawData[12], 0 });

        /// <summary>
        /// Region Code
        /// </summary>
        /// <remarks>For some reason the region code is only the last 2 bits of the first 4 bits of Byte 15</remarks>
        public RegionCode RegionCode
        {
            get
            {
                var code = (RegionCode)((RawData[15] & 0b00110000) >> 4);
                return Enum.IsDefined(typeof(RegionCode), code) ? code : RegionCode.Unknown;
            }
        }

        public SuicaLogEntry(TransitContext ctx)
        {
            _context = ctx;
            RawData = new byte[16];
        }

        public SuicaLogEntry(byte[] data, TransitContext ctx)
        {
            if (data.Length != 16)
                throw new ArgumentException("Invalid data provided.", nameof(data));

            _context = ctx;
            RawData = data;
        }

        /// <summary>
        /// Determines if this is this transaction is a Sale of Goods.
        /// </summary>
        /// <returns>Returns true if transaction is a Sale of Goods, false otherwise.</returns>
        public bool IsASaleOfGoods()
        {
            return TransactionType == TransactionType.SaleOfGoods
                   || TransactionType == TransactionType.BenefitCharge
                   || TransactionType == TransactionType.VoidSaleOfGoods
                   || TransactionType == TransactionType.EntranceGoods
                   || TransactionType == TransactionType.物現
                   || TransactionType == TransactionType.EntranceGoods_AdmissionAndGoods;
        }

        /// <summary>
        /// Determines if this is a transaction is the fare for a Bus or Tram
        /// </summary>
        /// <returns>True when this is a bus-realted transaction, false otherwise.</returns>
        public bool IsBusRelated()
        {
            return TransactionType == TransactionType.Bus_PiTaPa
                   || TransactionType == TransactionType.Bus_IruCa
                   || TransactionType == TransactionType.Bus_Deposit
                   || TransactionType == TransactionType.Bus_SpecialTicketPurchase;
        }

        public DateTime GetTimestampUtc()
        {
            var date = TransactionDate;
            var time = TransactionTime;

            if (time.HasValue)
            {
                var dt = new DateTime(date.Year, date.Month, date.Day, time.Value.Hour, time.Value.Minute, time.Value.Second, DateTimeKind.Unspecified);
                return TimeZoneInfo.ConvertTimeToUtc(dt, TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time"));
            }
            else
                return new DateTime(date.Year, date.Month, date.Day);
        }

        private async Task<IEnumerable<TrainStationModel>> GetEkiDataStationsFromSaibaneCode(byte region, byte line, byte station)
        {
            var saibane = await _context.SaibaneCodes.AsNoTracking().SingleOrDefaultAsync(sc => sc.RegionCode == region && sc.LineCode == line && sc.StationCode == station);
            var mappings = await _context.SaibaneCodeMappings
                .Include(sc => sc.EkiDataStation)
                    .ThenInclude(s => s.Line)
                        .ThenInclude(l => l.Company)
                            .ThenInclude(c => c.Status)
                .Include(sc => sc.EkiDataStation)
                    .ThenInclude(s => s.Line)
                        .ThenInclude(l => l.Status)
                .Include(sc => sc.EkiDataStation)
                    .ThenInclude(s => s.Status)
                .Include(sc => sc.Saibane)
                .AsNoTracking()
                .Where(sc => sc.RegionCode == region && sc.LineCode == line && sc.StationCode == station)
                .ToListAsync();

            var output = new List<TrainStationModel>();

            if (saibane != null && mappings.Count == 0)
                output.Add(new TrainStationModel(null, saibane));
            output.AddRange(mappings.Select(m => new TrainStationModel(m.EkiDataStation, m.Saibane)));

            return output;
        }

        /// <summary>
        /// Gets information about the Entry train station associated with this transaction
        /// </summary>
        /// <returns>Train station information or null if none is available.</returns>
        /// <exception cref="NotImplementedException">Multiple results were mapped to the entry Saibane Code.</exception>
        public async Task<TrainStationModel?> GetEntryStation()
        {
            if (IsASaleOfGoods() || IsBusRelated() || !EntryLineCode.HasValue || !EntryStationCode.HasValue) return null;

            var mappings = await GetEkiDataStationsFromSaibaneCode((byte)RegionCode, EntryLineCode.Value, EntryStationCode.Value);

            //Nothing found
            if (!mappings.Any())
                return null;

            //The only match is the right one
            if (mappings.Count() == 1)
                return mappings.First();

            //Oh no... Maybe pick the best one based on the Entry & Exit?
            //TODO: figure out how to pick a station if more than one result
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets information about the Exit train station associated with this transaction
        /// </summary>
        /// <returns>Train station information or null if none is available.</returns>
        /// <exception cref="NotImplementedException">Multiple results were mapped to the entry Saibane Code.</exception>
        public async Task<TrainStationModel?> GetExitStation()
        {
            if (IsASaleOfGoods() || IsBusRelated() || !ExitLineCode.HasValue || !ExitStationCode.HasValue) return null;

            var mappings = await GetEkiDataStationsFromSaibaneCode((byte)RegionCode, ExitLineCode.Value, ExitStationCode.Value);

            //Nothing found
            if (!mappings.Any())
                return null;

            //The only match is the right one
            if (mappings.Count() == 1)
                return mappings.First();

            //Oh no... Maybe pick the best one based on the Entry & Exit?
            //TODO: figure out how to pick a station if more than one result
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets information about the bus stop associated with this transaction.
        /// </summary>
        /// <returns>Bus stop information or null if there is no information available.</returns>
        public BusStopModel? GetBusStop()
        {
            if (!IsBusRelated() || !BusOperatorCode.HasValue || !BusStopCode.HasValue) return null;

            var result = _context.IruCaBusStops.AsNoTracking().SingleOrDefault(b => b.LineCode == BusOperatorCode.Value && b.StationCode == BusStopCode.Value);
            return result != null ? new BusStopModel(result) : null;   
        }
    }
}

using System.Buffers.Binary;

namespace Solidus.SuicaTools.CardReaderCLI
{
    internal class FelicaCommandBuilder
    {
        public byte Command { get; set; }
        public byte[] IDm { get; set; }
        public byte[] Data { get; set; }
        public int Length => IDm.Length + Data.Length + 2;

        public FelicaCommandBuilder(byte cmd)
        {
            Command = cmd;
            IDm = Array.Empty<byte>();
            Data = Array.Empty<byte>();
        }

        public FelicaCommandBuilder(byte cmd, byte[] idm, byte[] data)
        {
            Command = cmd;
            IDm = idm;
            Data = data;
        }

        public byte[] GetRawCommand()
        {
            if (Length > 255)
                throw new ArgumentOutOfRangeException(nameof(Length), "Command Length may not exceed 255 bytes.");

            var cmd = new List<byte>(Length);
            cmd.Add((byte)Length);
            cmd.Add(Command);
            if (IDm.Length > 0)
                cmd.AddRange(IDm);
            if (Data.Length > 0)
                cmd.AddRange(Data);

            return cmd.ToArray();
        }

        /// <summary>
        /// Generates a command to acquire the Manufacturer ID (IDm) and Manufacturer Parameters (PMm) of the card.
        /// </summary>
        /// <param name="systemCode">Specifies a System Code otherwise defaults to a wildcard the gets System 0 first if its available or the next available System.</param>
        /// <param name="requestCode">Specifies Request Data. 0x00 for No Request. 0x01 for System Code. 0x02 for Communication Performance.</param>
        /// <param name="timeSlot">Specifies maximum number of timeslots to expect a response in.</param>
        /// <returns>Command byte array</returns>
        /// <remarks>See FeliCa Card User's Manual Excepted Edition Section 4.4.2 for full details.</remarks>
        public static byte[] Polling(ushort systemCode = 0xFFFF, byte requestCode = 0x00, byte timeSlot = 0x00)
        {
            var builder = new FelicaCommandBuilder(FelicaCommandCode.Polling);
            var data = new List<byte>(5);
            var s = new Span<byte>(new byte[2]);
            BinaryPrimitives.WriteUInt16LittleEndian(s, systemCode);
            data.AddRange(s.ToArray());
            data.Add(requestCode);
            data.Add(timeSlot);
            builder.Data = data.ToArray();

            return builder.GetRawCommand();
        }

        /// <summary>
        /// Generates a command to enumerate the System Codes available on the card.
        /// </summary>
        /// <param name="IDm">Manufacturer ID</param>
        /// <returns>Command byte array</returns>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="IDm"/> is not exactly 8 bytes in length.</exception>
        /// <remarks>See FeliCa Card User's Manual Excepted Edition Section 4.4.8 for full details.</remarks>
        public static byte[] RequestSystemCode(byte[] IDm)
        {
            if (IDm.Length != 8)
                throw new ArgumentOutOfRangeException(nameof(IDm), "Manufacturer ID must be 8 bytes in length.");

            var builder = new FelicaCommandBuilder(FelicaCommandCode.RequestSystemCode);
            builder.IDm = IDm;
            return builder.GetRawCommand();
        }

        /// <summary>
        /// Generates a command to verify the existence of a particular Area and Service.
        /// Running this command successfully returns the Key Version when the Area / Service exists and 0xFFFF if the Area / service doesn't exist.
        /// </summary>
        /// <param name="IDm">Manufacturer ID</param>
        /// <param name="nodeCodeList">List of Areas/Services to check</param>
        /// <returns>Command byte array</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <remarks>See FeliCa Card User's Manual Excepted Edition Section 4.4.3 for full details.</remarks>
        public static byte[] RequestService(byte[] IDm, params ushort[] nodeCodeList)
        {
            if (IDm.Length != 8)
                throw new ArgumentOutOfRangeException(nameof(IDm), "Manufacturer ID must be 8 bytes in length.");
            if (nodeCodeList.Length == 0 || nodeCodeList.Length > 32)
                throw new ArgumentOutOfRangeException(nameof(nodeCodeList), "Must request betweent 1 and 32 Services.");

            var builder = new FelicaCommandBuilder(FelicaCommandCode.RequestService);
            builder.IDm = IDm;

            var data = new List<byte>();
            data.Add((byte)nodeCodeList.Count());

            foreach (ushort service in nodeCodeList)
            {
                var s = new Span<byte>(new byte[2]);
                BinaryPrimitives.WriteUInt16LittleEndian(s, service);
                data.AddRange(s.ToArray());
            }
            builder.Data = data.ToArray();

            return builder.GetRawCommand();
        }

        /// <summary>
        /// Generates a command to read unencrypted Block Data the specified Service.
        /// </summary>
        /// <param name="IDm">Manufacturer ID</param>
        /// <param name="serviceCode">Services to read Block from</param>
        /// <param name="blockNumber">Block numbers to read</param>
        /// <param name="cashbackAccess">Specifies we're trying to read from a Cashback Access Purse</param>
        /// <returns>Command byte array</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <remarks>
        /// Blocks are currently read 1 at a time because I don't know how to determine the maximum receive size yet.
        /// See FeliCa Card User's Manual Excepted Edition Sections 4.4.5 & 4.2.1 for full details.
        /// </remarks>
        public static byte[] ReadWithoutEncryption(byte[] IDm, ushort serviceCode, ushort blockNumber, bool cashbackAccess = false)
        {
            if (IDm.Length != 8)
                throw new ArgumentOutOfRangeException(nameof(IDm), "Manufactuerer ID must be 8 bytes in length.");

            var builder = new FelicaCommandBuilder(FelicaCommandCode.ReadWithoutEncryption);
            builder.IDm = IDm;

            var data = new List<byte>();
            data.Add((byte)0x01); //Number of Services

            var s1 = new Span<byte>(new byte[2]);
            BinaryPrimitives.WriteUInt16LittleEndian(s1, serviceCode);
            data.AddRange(s1.ToArray());

            data.Add((byte)0x01); //Number of Blocks

            //length (1 bit), access mode (3 bits), service code list order (4 bits), block number or key version (1 or 2 bytes)
            //length: 2-byte block list elements - 0b10000000
            //length: 3-byte block list elements - 0b00000000
            //access mode: read/write            - 0b00001000
            //access mode: cashback access       - 0b00000000
            var blockNumBytes = blockNumber <= 255 ? 1 : 2;
            var blockListElementSize = blockNumBytes == 1 ? (byte)0b10000000 : (byte)0b00000000;
            var access = cashbackAccess ? (byte)0b00010000 : (byte)0b00000000;
            var serviceCodeListOrder = (byte)0b00000000;
            var blockListElementHeader = (byte)(blockListElementSize | access | serviceCodeListOrder);

            
            var s2 = new Span<byte>(new byte[2]);
            BinaryPrimitives.WriteUInt16LittleEndian(s2, blockNumber);

            data.Add(blockListElementHeader);
            if (blockNumBytes == 2)
                data.AddRange(s2.ToArray());
            else
                data.Add(s2.ToArray().First());

            builder.Data = data.ToArray();
            return builder.GetRawCommand();
        }
    }
}
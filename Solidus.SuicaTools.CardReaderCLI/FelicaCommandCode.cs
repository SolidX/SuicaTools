namespace Solidus.SuicaTools.CardReaderCLI
{
    internal static class FelicaCommandCode
    {
        /// <summary>
        /// Use this command to acquire and identify a card
        /// </summary>
        public const byte Polling = 0x00;
        /// <summary>
        /// Use this command to verify the existence of Area or Service, and to acquire Key Version
        /// </summary>
        public const byte RequestService = 0x02;
        /// <summary>
        /// Use this command to verify the existence of a card and its Mode.
        /// </summary>
        public const byte RequestRepsonse = 0x04;
        /// <summary>
        /// Use this command to Read Block Data without authenticating
        /// </summary>
        public const byte ReadWithoutEncryption = 0x06;
        /// <summary>
        /// Use this command to Write Block Data without authenticating 
        /// </summary>
        public const byte WriteWithoutEncryption = 0x08;
        /// <summary>
        /// Use this command to authenticate a card with DES
        /// </summary>
        public const byte Auth1 = 0x10;
        /// <summary>
        /// Use this command to allow a card to authenticate a Reader/Writer with DES
        /// </summary>
        public const byte Auth2 = 0x12;
        /// <summary>
        /// Use this command to read Block Data from a Service requiring Authentication
        /// </summary>
        public const byte ReadSecure = 0x14;
        /// <summary>
        /// Use this command to write Block Data from a Service requiring Authentication
        /// </summary>
        public const byte WriteSecure = 0x16;
        /// <summary>
        /// Use this command to acquire Area Code and Service Code
        /// </summary>
        public const byte SearchServiceCode = 0x0a;
        /// <summary>
        /// Use this command to acquire System Code registered to the card
        /// </summary>
        public const byte RequestSystemCode = 0x0c;
        /// <summary>
        /// Use this command to acquire Node Property
        /// </summary>
        public const byte GetNodeProperty = 0x28;
        /// <summary>
        /// Use this command to verify the existence of Area or Service, and to acquire Key Version
        /// </summary>
        public const byte RequestService_v2 = 0x32;
        /// <summary>
        /// Use this command to acqurie the setup information in System
        /// </summary>
        public const byte SystemStatus = 0x38;
        /// <summary>
        /// Use this command to perform internal authentication of Communication-with-MAC enabled Service and to write Block Data
        /// </summary>
        public const byte InternalAuthRead = 0x34;
        /// <summary>
        /// Use this command to perform external authentication of Communication-with-MAC enabled Service and to write Block Data
        /// </summary>
        public const byte ExternalAuthWrite = 0x36;
        /// <summary>
        /// Use this command to acquire the card OS version
        /// </summary>
        public const byte RequestSpecVersion = 0x3c;
        /// <summary>
        /// Use this commadn to reset Mode to Mode 0
        /// </summary>
        public const byte ResetMode = 0x3e;
        /// <summary>
        /// Use this command to authenticate a card with AES
        /// </summary>
        public const byte Auth1_v2 = 0x40;
        /// <summary>
        /// Use this command to allow a card to authenticate a Reader/Writer with AES
        /// </summary>
        public const byte Auth2_v2 = 0x42;
        /// <summary>
        /// Use this command to read Block Data from a Service requiring Authentication
        /// </summary>
        public const byte Read_v2 = 0x44;
        /// <summary>
        /// Use this command to write Block Data from a Service requiring Authentication
        /// </summary>
        public const byte Write_v2 = 0x46;
        /// <summary>
        /// Use this command to update Random ID (IDr)
        /// </summary>
        public const byte UpdateIDr = 0x4c;
        /// <summary>
        /// Use this command to set Node Property
        /// </summary>
        public const byte SetNodeProperty = 0x78;
    }
}

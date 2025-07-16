namespace Solidus.SuicaTools.CardReaderCLI
{
    internal static class FelicaSystemCode
    {
        public const ushort Any = 0xFFFF;
        public const ushort Common = 0xFE00;
        /// <summary>
        /// Suica, PASMO, ICOCA, PiTaPa, TOICA, Mobile Suica
        /// </summary>
        public const ushort Saibane = 0x0003;

        public const ushort Edy = Common;
        public const ushort Waon = Common;
        public const ushort Suica = Saibane;
        public const ushort QuicPay = 0x04C1;
        public const ushort Setamaru = 0x2B80;
        public const ushort IruCa = 0xDE80;
    }
}

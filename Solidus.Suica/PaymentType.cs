namespace Solidus.SuicaTools
{
    /// <see href="https://github.com/kjj6198/swift-core-nfc-reader/blob/master/nfc/FeliCaReader/FeliCaCard.swift#L13"/>
    /// <seealso href="http://jennychan.web.fc2.com/format/suica.html"/>
    public enum PaymentType
    {
        /// <summary>
        /// Payment Type is unknown or invalid.
        /// </summary>
        /// <remarks>This isn't actually part of the spec.</remarks>
        Unknown = 0xFFF,

        /// <summary>
        /// Cash or None
        /// </summary>
        Default = 0x00,

        /// <summary>
        /// VIEW
        /// </summary>
        VIEW = 0x02,

        /// <summary>
        /// PiTaPa
        /// </summary>
        PiTaPa = 0x0B,

        /// <summary>
        /// Credit Card
        /// </summary>
        CreditCard = 0x0C,

        /// <summary>
        /// Pasmo with auto-charge
        /// </summary>
        Pasmo = 0x0D,

        /// <summary>
        /// nimoca
        /// </summary>
        Nimoca = 0x13,

        /// <summary>
        /// Mobile Suica (not VIEW payment)
        /// </summary>
        MobileSuica = 0x3F
    }
}

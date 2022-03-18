using Solidus.SuicaTools.Localization;
using System.ComponentModel;

namespace Solidus.SuicaTools
{
    /// <see href="https://github.com/kjj6198/swift-core-nfc-reader/blob/master/nfc/FeliCaReader/FeliCaCard.swift#L13"/>
    /// <seealso href="http://jennychan.web.fc2.com/format/suica.html"/>
    [TypeConverter(typeof(EnumLocalizedDescriptionTypeConverter))]
    public enum PaymentType
    {
        /// <summary>
        /// Payment Type is unknown or invalid.
        /// </summary>
        /// <remarks>This isn't actually part of the spec.</remarks>
        [LocalizedDescription("Unknown", typeof(Resources.PaymentType))]
        Unknown = 0xFFF,

        /// <summary>
        /// Cash or None
        /// </summary>
        [LocalizedDescription("Default", typeof(Resources.PaymentType))]
        Default = 0x00,

        /// <summary>
        /// VIEW
        /// </summary>
        [LocalizedDescription("VIEW", typeof(Resources.PaymentType))]
        VIEW = 0x02,

        /// <summary>
        /// PiTaPa
        /// </summary>
        [LocalizedDescription("PiTaPa", typeof(Resources.PaymentType))]
        PiTaPa = 0x0B,

        /// <summary>
        /// Credit Card
        /// </summary>
        [LocalizedDescription("CreditCard", typeof(Resources.PaymentType))]
        CreditCard = 0x0C,

        /// <summary>
        /// Pasmo with auto-charge
        /// </summary>
        [LocalizedDescription("Pasmo", typeof(Resources.PaymentType))]
        Pasmo = 0x0D,

        /// <summary>
        /// nimoca
        /// </summary>
        [LocalizedDescription("Nimoca", typeof(Resources.PaymentType))]
        Nimoca = 0x13,

        /// <summary>
        /// Mobile Suica (not VIEW payment)
        /// </summary>
        [LocalizedDescription("MobileSuica", typeof(Resources.PaymentType))]
        MobileSuica = 0x3F
    }
}

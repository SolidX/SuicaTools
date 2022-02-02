namespace SuicaDe
{
    /// <see href="http://jennychan.web.fc2.com/format/suica.html"/>
    /// <remarks>
    /// This is the only source that seems to know what this field is so I'm just going to trust that it's true.
    /// I'm not even going to bother trying to name these members in English in case I'm wrong though.
    /// </remarks>
    public enum EntryExitType
    {
        /// <summary>
        /// Entry/Exit type is invalid or unknown
        /// </summary>
        /// <remarks>This isn't actually part of the spec.</remarks>
        Unknown = 0xFFF,

        /// <summary>
        /// Not a transit entry/exit
        /// </summary>
        /// <remarks>I'm guessing this is for non-transit realted transactions</remarks>
        NotApplicable = 0x00,

        /// <summary>
        /// Platform Ticket(?)
        /// </summary>
        /// <seealso href="https://en.wikipedia.org/wiki/Platform_ticket"/>
        入場 = 0x01,

        /// <summary>
        /// Regular Entry & Regular Exit
        /// </summary>
        入場_出場 = 0x02,

        /// <summary>
        /// Commuter Pass Entry & Regular Exit
        /// </summary>
        定期入場_出場 = 0x03,

        /// <summary>
        /// Regular Entry & Commuter Pass Exit
        /// </summary>
        入場_定期出場 = 0x04,

        //TODO: 0x05 exists but it's not documented anywhere so no one knows what it does.

        /// <summary>
        /// Exit via Ticket Window
        /// </summary>
        窓口出場 = 0x0E,

        /// <summary>
        /// Bus Entry/Exit
        /// </summary>
        入場_出場_バス等 = 0x0F,

        /// <summary>
        /// Commuter Pass Fee 
        /// </summary>
        料金定期入場_料金出場 = 0x12,

        /// <summary>
        /// Entry/Exit Transfer Discount
        /// </summary>
        入場_出場_乗継割引 = 0x17,

        /// <summary>
        /// Bus Entry/Exit Transfer Discount
        /// </summary>
        バス等乗継割引 = 0x21
    }
}

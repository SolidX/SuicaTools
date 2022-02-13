namespace Solidus.SuicaTools
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

        /// <remarks>Entry only???</remarks>
        Entry = 0x01,

        /// <summary>
        /// Regular Entry & Regular Exit
        /// </summary>
        Entry_Exit = 0x02,

        /// <summary>
        /// Commuter Pass Entry & Regular Exit
        /// </summary>
        PassEntry_Exit = 0x03,

        /// <summary>
        /// Regular Entry & Commuter Pass Exit
        /// </summary>
        Entry_PassExit = 0x04,

        //TODO: 0x05 exists but it's not documented anywhere so no one knows what it does.

        /// <summary>
        /// Exit via Ticket Window
        /// </summary>
        TicketCounter_Exit = 0x0E,

        /// <summary>
        /// Bus Entry/Exit
        /// </summary>
        Entry_Exit_Bus = 0x0F,

        /// <summary>
        /// Commuter Pass Fee 
        /// </summary>
        CommuterPassFee = 0x12,

        /// <summary>
        /// Entry/Exit Transfer Discount
        /// </summary>
        TransferDiscount = 0x17,

        /// <summary>
        /// Bus Entry/Exit Transfer Discount
        /// </summary>
        BusTransferDiscount = 0x21
    }
}

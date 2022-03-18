using Solidus.SuicaTools.Localization;
using System.ComponentModel;

namespace Solidus.SuicaTools
{
    /// <see href="http://jennychan.web.fc2.com/format/suica.html"/>
    /// <remarks>
    /// This is the only source that seems to know what this field is so I'm just going to trust that it's true.
    /// I'm not even going to bother trying to name these members in English in case I'm wrong though.
    /// </remarks>
    [TypeConverter(typeof(EnumLocalizedDescriptionTypeConverter))]
    public enum EntryExitType
    {
        /// <summary>
        /// Entry/Exit type is invalid or unknown
        /// </summary>
        /// <remarks>This isn't actually part of the spec.</remarks>
        [LocalizedDescription("Unknown", typeof(Resources.EntryExitType))]
        Unknown = 0xFFF,

        /// <summary>
        /// Not a transit entry/exit
        /// </summary>
        /// <remarks>I'm guessing this is for non-transit realted transactions</remarks>
        [LocalizedDescription("NotApplicable", typeof(Resources.EntryExitType))]
        NotApplicable = 0x00,

        /// <remarks>Entry only???</remarks>
        [LocalizedDescription("Entry", typeof(Resources.EntryExitType))]
        Entry = 0x01,

        /// <summary>
        /// Regular Entry & Regular Exit
        /// </summary>
        [LocalizedDescription("Entry_Exit", typeof(Resources.EntryExitType))]
        Entry_Exit = 0x02,

        /// <summary>
        /// Commuter Pass Entry & Regular Exit
        /// </summary>
        [LocalizedDescription("PassEntry_Exit", typeof(Resources.EntryExitType))]
        PassEntry_Exit = 0x03,

        /// <summary>
        /// Regular Entry & Commuter Pass Exit
        /// </summary>
        [LocalizedDescription("Entry_PassExit", typeof(Resources.EntryExitType))]
        Entry_PassExit = 0x04,

        //TODO: 0x05 exists but it's not documented anywhere so no one knows what it does.

        /// <summary>
        /// Exit via Ticket Window
        /// </summary>
        [LocalizedDescription("TicketCounter_Exit", typeof(Resources.EntryExitType))]
        TicketCounter_Exit = 0x0E,

        /// <summary>
        /// Bus Entry/Exit
        /// </summary>
        [LocalizedDescription("Entry_Exit_Bus", typeof(Resources.EntryExitType))]
        Entry_Exit_Bus = 0x0F,

        /// <summary>
        /// Commuter Pass Fee 
        /// </summary>
        [LocalizedDescription("CommuterPassFee", typeof(Resources.EntryExitType))]
        CommuterPassFee = 0x12,

        /// <summary>
        /// Entry/Exit Transfer Discount
        /// </summary>
        [LocalizedDescription("TransferDiscount", typeof(Resources.EntryExitType))]
        TransferDiscount = 0x17,

        /// <summary>
        /// Bus Entry/Exit Transfer Discount
        /// </summary>
        [LocalizedDescription("BusTransferDiscount", typeof(Resources.EntryExitType))]
        BusTransferDiscount = 0x21
    }
}

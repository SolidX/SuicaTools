using Solidus.SuicaTools.Localization;
using System.ComponentModel;

namespace Solidus.SuicaTools
{
    [TypeConverter(typeof(EnumLocalizedDescriptionTypeConverter))]
    public enum TransactionType
    {
        /// <summary>
        /// Processing Type is invalid or unknown
        /// </summary>
        /// <remarks>This isn't actually part of the spec.</remarks>
        [LocalizedDescription("Unknown", typeof(Resources.TransactionType))]
        Unknown = 0xFFF,

        /// <summary>
        /// 運賃支払(改札出場)
        /// </summary>
        [LocalizedDescription("FarePayment", typeof(Resources.TransactionType))]
        FarePayment = 0x01,

        /// <summary>
        /// チャージ
        /// </summary>
        /// <remarks>Adding money to the card.</remarks>
        [LocalizedDescription("Charge", typeof(Resources.TransactionType))]
        Charge = 0x02,

        /// <summary>
        /// 券購(磁気券購入)
        /// </summary>
        [LocalizedDescription("TicketPurchase", typeof(Resources.TransactionType))]
        TicketPurchase = 0x03,

        /// <summary>
        /// 精算
        /// </summary>
        [LocalizedDescription("Settlement", typeof(Resources.TransactionType))]
        Settlement = 0x04,

        /// <summary>
        /// 精算 (入場精算)
        /// </summary>
        [LocalizedDescription("FareAdjustment", typeof(Resources.TransactionType))]
        FareAdjustment = 0x05,

        /// <summary>
        /// 窓出 (改札窓口処理)
        /// </summary>
        [LocalizedDescription("TicketOfficeExit", typeof(Resources.TransactionType))]
        TicketOfficeExit = 0x06,

        /// <summary>
        /// 新規 (新規発行)
        /// </summary>
        /// <remarks>Issuance of a new card</remarks>
        [LocalizedDescription("NewIssue", typeof(Resources.TransactionType))]
        NewIssue = 0x07,

        /// <summary>
        /// 控除 (窓口控除)
        /// </summary>
        [LocalizedDescription("TicketOfficeDeduction", typeof(Resources.TransactionType))]
        TicketOfficeDeduction = 0x08,

        /// <summary>
        /// バス (PiTaPa系)
        /// </summary>
        [LocalizedDescription("Bus_PiTaPa", typeof(Resources.TransactionType))]
        Bus_PiTaPa = 0x0D,

        /// <summary>
        /// バス (IruCa系)
        /// </summary>
        [LocalizedDescription("Bus_IruCa", typeof(Resources.TransactionType))]
        Bus_IruCa = 0x0F,

        /// <summary>
        /// 再発 (再発行処理)
        /// </summary>
        /// <remarks>Reissuance of a card</remarks>
        [LocalizedDescription("Reissue", typeof(Resources.TransactionType))]
        Reissue = 0x11,

        /// <summary>
        /// 支払 (新幹線利用)
        /// </summary>
        [LocalizedDescription("ShinkansenPayment", typeof(Resources.TransactionType))]
        ShinkansenPayment = 0x13,

        /// <summary>
        /// 入A (入場時オートチャージ)
        /// </summary>
        [LocalizedDescription("Entrance_AutoCharge", typeof(Resources.TransactionType))]
        Entrance_AutoCharge = 0x14,

        /// <summary>
        /// 出A (出場時オートチャージ)
        /// </summary>
        [LocalizedDescription("Exit_AutoCharge", typeof(Resources.TransactionType))]
        Exit_AutoCharge = 0x15,

        /// <summary>
        /// 入金 (バスチャージ)
        /// </summary>
        [LocalizedDescription("Bus_Deposit", typeof(Resources.TransactionType))]
        Bus_Deposit = 0x1F,

        /// <summary>
        /// 券購 (バス路面電車企画券購入)
        /// </summary>
        [LocalizedDescription("Bus_SpecialTicketPurchase", typeof(Resources.TransactionType))]
        Bus_SpecialTicketPurchase = 0x23,

        /// <summary>
        /// 物販
        /// </summary>
        [LocalizedDescription("SaleOfGoods", typeof(Resources.TransactionType))]
        SaleOfGoods = 0x46,

        /// <summary>
        /// 特典 (特典チャージ)
        /// </summary>
        [LocalizedDescription("BenefitCharge", typeof(Resources.TransactionType))]
        BenefitCharge = 0x48,

        /// <summary>
        /// 入金 (レジ入金)
        /// </summary>
        [LocalizedDescription("Deposit_Registration", typeof(Resources.TransactionType))]
        Deposit_Registration = 0x49,

        /// <summary>
        /// 物販取消
        /// </summary>
        [LocalizedDescription("VoidSaleOfGoods", typeof(Resources.TransactionType))]
        VoidSaleOfGoods = 0x4A,

        /// <summary>
        /// 入物 (入場物販)
        /// </summary>
        [LocalizedDescription("EntranceGoods", typeof(Resources.TransactionType))]
        EntranceGoods = 0x4B,

        /// <summary>
        /// 物現 (現金併用物販)
        /// </summary>
        /// <remarks>I can't even begin to make sense of this so I give up on trying to name it in English</remarks>
        [LocalizedDescription("物現", typeof(Resources.TransactionType))]
        物現 = 0xC6,

        /// <summary>
        /// 入物 (入場現金併用物販)
        /// </summary>
        [LocalizedDescription("EntranceGoods_AdmissionAndGoods", typeof(Resources.TransactionType))]
        EntranceGoods_AdmissionAndGoods = 0xCB,

        /// <summary>
        /// 精算 (他社精算)
        /// </summary>
        [LocalizedDescription("ThirdParty_Purchase", typeof(Resources.TransactionType))]
        ThirdParty_Purchase = 0x84,

        /// <summary>
        /// 精算 (他社入場精算)
        /// </summary>
        [LocalizedDescription("ThirdParty_Admission", typeof(Resources.TransactionType))]
        ThirdParty_Admission = 0x85
    }
}

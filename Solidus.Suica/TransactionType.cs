namespace Solidus.SuicaTools
{
    public enum TransactionType
    {
        /// <summary>
        /// Processing Type is invalid or unknown
        /// </summary>
        /// <remarks>This isn't actually part of the spec.</remarks>
        Unknown = 0xFFF,

        /// <summary>
        /// 運賃支払(改札出場)
        /// </summary>
        FarePayment = 1,

        /// <summary>
        /// チャージ
        /// </summary>
        /// <remarks>Adding money to the card.</remarks>
        Charge = 2,

        /// <summary>
        /// 券購(磁気券購入)
        /// </summary>
        TicketPurchase = 3,

        /// <summary>
        /// 精算
        /// </summary>
        Settlement = 4,

        /// <summary>
        /// 精算 (入場精算)
        /// </summary>
        FareAdjustment = 5,

        /// <summary>
        /// 窓出 (改札窓口処理)
        /// </summary>
        TicketOfficeExit = 6,

        /// <summary>
        /// 新規 (新規発行)
        /// </summary>
        /// <remarks>Issuance of a new card</remarks>
        NewIssue = 7,

        /// <summary>
        /// 控除 (窓口控除)
        /// </summary>
        TicketOfficeDeduction = 8,

        /// <summary>
        /// バス (PiTaPa系)
        /// </summary>
        Bus_PiTaPa = 13,

        /// <summary>
        /// バス (IruCa系)
        /// </summary>
        Bus_IruCa = 15,

        /// <summary>
        /// 再発 (再発行処理)
        /// </summary>
        /// <remarks>Reissuance of a card</remarks>
        Reissue = 17,

        /// <summary>
        /// 支払 (新幹線利用)
        /// </summary>
        ShinkansenPayment = 19,

        /// <summary>
        /// 入A (入場時オートチャージ)
        /// </summary>
        Entrance_AutoCharge = 20,

        /// <summary>
        /// 出A (出場時オートチャージ)
        /// </summary>
        Exit_AutoCharge = 21,

        /// <summary>
        /// 入金 (バスチャージ)
        /// </summary>
        Bus_Deposit = 31,

        /// <summary>
        /// 券購 (バス路面電車企画券購入)
        /// </summary>
        Bus_SpecialTicketPurchase = 35,

        /// <summary>
        /// 物販
        /// </summary>
        SaleOfGoods = 70,

        /// <summary>
        /// 特典 (特典チャージ)
        /// </summary>
        BenefitCharge = 72,

        /// <summary>
        /// 入金 (レジ入金)
        /// </summary>
        Deposit_Registration = 73,

        /// <summary>
        /// 物販取消
        /// </summary>
        VoidSaleOfGoods = 74,

        /// <summary>
        /// 入物 (入場物販)
        /// </summary>
        EntranceGoods = 75,

        /// <summary>
        /// 物現 (現金併用物販)
        /// </summary>
        /// <remarks>I can't even begin to make sense of this so I give up on trying to name it in English</remarks>
        物現 = 198,

        /// <summary>
        /// 入物 (入場現金併用物販)
        /// </summary>
        EntranceGoods_AdmissionAndGoods = 203,

        /// <summary>
        /// 精算 (他社精算)
        /// </summary>
        Settlement_External = 132,

        /// <summary>
        /// 精算 (他社入場精算)
        /// </summary>
        Settlement_ExternalAdmission = 133
    }
}

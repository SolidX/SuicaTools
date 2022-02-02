namespace SuicaDe
{
    public enum TerminalType
    {
        /// <summary>
        /// TerminalType is invalid or unknown
        /// </summary>
        /// <remarks>This isn't actually part of the spec.</remarks>
        Unknown = 0xFFF,

        /// <summary>
        /// 精算機 / せいさんき / fare adjustment machine
        /// </summary>
        FareAdjustmentMachine = 3,

        /// <summary>
        /// 携帯型端末 / Portable Terminal?
        /// </summary>
        PortableTerminal = 4,

        /// <summary>
        /// 車載端末 / On-board Terminal?
        /// </summary>
        OnboardTerminal = 5,

        /// <summary>
        /// 券売機 / けんばいき / Ticket vending machine
        /// </summary>
        TicketVendingMachineA = 7,

        /// <summary>
        /// 券売機 / けんばいき / Ticket vending machine
        /// </summary>
        TicketVendingMachineB = 8,

        /// <summary>
        /// 入金機
        /// </summary>
        /// <remarks>Yeah I can't make heads or tails of this one.</remarks>
        DepositMachine = 9,

        /// <summary>
        /// 券売機 / けんばいき / Ticket vending machine
        /// </summary>
        TicketVendingMachineC = 18,
        
        /// <summary>
        /// 券売機等 / Ticket vending machines?
        /// </summary>
        /// <remarks>Have I mentioned I don't speak japanese?</remarks>
        TicketVendingMachinesA = 20,

        /// <summary>
        /// 券売機等 / Ticket vending machines?
        /// </summary>
        /// <remarks>Have I mentioned I don't speak japanese?</remarks>
        TicketVendingMachinesB = 21,

        /// <summary>
        /// 改札機 / かいさつき
        /// </summary>
        FareGate = 22,

        /// <summary>
        /// 簡易改札機
        /// </summary>
        SimpleFareGate = 23,

        /// <summary>
        /// 窓口端末
        /// </summary>
        TicketWindowTerminalA = 24,
        
        /// <summary>
        /// 窓口端末
        /// </summary>
        TicketWindowTerminalB = 25,

        /// <summary>
        /// 改札端末
        /// </summary>
        FareGateTerminal = 26,

        /// <summary>
        /// 携帯電話 / けいたいでんわ
        /// </summary>
        MobilePhone = 27,

        /// <summary>
        /// 乗継精算機 / のりつぎせいさんき
        /// </summary>
        TransferFareAdjustmentMachine = 28,

        /// <summary>
        /// 連絡改札機 / れんらくかいさつき
        /// </summary>
        ConnectingFareGate = 29,

        /// <summary>
        /// 簡易入金機
        /// </summary>
        EasyDepositMachine = 31,

        /// <summary>
        /// VIEW ALTTE
        /// </summary>
        /// <seealso href="https://www.jreast.co.jp/card/guide/atm/"/>
        ViewAltteA = 70,

        /// <summary>
        /// VIEW ALTTE
        /// </summary>
        /// <seealso href="https://www.jreast.co.jp/card/guide/atm/"/>
        ViewAltteB = 72,

        /// <summary>
        /// 物販端末
        /// </summary>
        PointOfSale = 199,

        /// <summary>
        /// 自販機 / じはんき
        /// </summary>
        VendingMachine = 200
    }
}

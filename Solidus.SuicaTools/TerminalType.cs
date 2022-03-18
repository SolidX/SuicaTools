using Solidus.SuicaTools.Localization;
using System.ComponentModel;

namespace Solidus.SuicaTools
{
    [TypeConverter(typeof(EnumLocalizedDescriptionTypeConverter))]
    public enum TerminalType
    {
        /// <summary>
        /// TerminalType is invalid or unknown
        /// </summary>
        /// <remarks>This isn't actually part of the spec.</remarks>
        [LocalizedDescription("Unknown", typeof(Resources.TerminalType))]
        Unknown = 0xFFF,

        /// <summary>
        /// 精算機 / せいさんき / fare adjustment machine
        /// </summary>
        [LocalizedDescription("FareAdjustmentMachine", typeof(Resources.TerminalType))]
        FareAdjustmentMachine = 3,

        /// <summary>
        /// 携帯型端末 / Portable Terminal?
        /// </summary>
        [LocalizedDescription("PortableTerminal", typeof(Resources.TerminalType))]
        PortableTerminal = 4,

        /// <summary>
        /// 車載端末 / On-board Terminal?
        /// </summary>
        [LocalizedDescription("OnboardTerminal", typeof(Resources.TerminalType))]
        OnboardTerminal = 5,

        /// <summary>
        /// 券売機 / けんばいき / Ticket vending machine
        /// </summary>
        /// <remarks>There are multiple kinds of these.</remarks>
        [LocalizedDescription("TicketVendingMachineA", typeof(Resources.TerminalType))]
        TicketVendingMachineA = 7,

        /// <summary>
        /// 券売機 / けんばいき / Ticket vending machine
        /// </summary>
        /// <remarks>There are multiple kinds of these.</remarks>
        [LocalizedDescription("TicketVendingMachineB", typeof(Resources.TerminalType))]
        TicketVendingMachineB = 8,

        /// <summary>
        /// 入金機
        /// </summary>
        /// <remarks>Yeah I can't make heads or tails of this one.</remarks>
        [LocalizedDescription("DepositMachine", typeof(Resources.TerminalType))]
        DepositMachine = 9,

        /// <summary>
        /// 券売機 / けんばいき / Ticket vending machine
        /// </summary>
        /// <remarks>There are multiple kinds of these.</remarks>
        [LocalizedDescription("TicketVendingMachineC", typeof(Resources.TerminalType))]
        TicketVendingMachineC = 18,

        /// <summary>
        /// 券売機等 / Ticket vending machines?
        /// </summary>
        /// <remarks>I don't know how these are different from the other TVMs but these are TVM-like and there are also multiple kinds of them.</remarks>
        [LocalizedDescription("TicketVendingMachineX", typeof(Resources.TerminalType))]
        TicketVendingMachineX = 20,

        /// <summary>
        /// 券売機等 / Ticket vending machines?
        /// </summary>
        /// <remarks>I don't know how these are different from the other TVMs but these are TVM-like and there are also multiple kinds of them.</remarks>
        [LocalizedDescription("TicketVendingMachineY", typeof(Resources.TerminalType))]
        TicketVendingMachineY = 21,

        /// <summary>
        /// 改札機 / かいさつき
        /// </summary>
        [LocalizedDescription("FareGate", typeof(Resources.TerminalType))]
        FareGate = 22,

        /// <summary>
        /// 簡易改札機
        /// </summary>
        [LocalizedDescription("SimpleFareGate", typeof(Resources.TerminalType))]
        SimpleFareGate = 23,

        /// <summary>
        /// 窓口端末
        /// </summary>
        [LocalizedDescription("TicketWindowTerminalA", typeof(Resources.TerminalType))]
        TicketWindowTerminalA = 24,
        
        /// <summary>
        /// 窓口端末
        /// </summary>
        [LocalizedDescription("TicketWindowTerminalB", typeof(Resources.TerminalType))]
        TicketWindowTerminalB = 25,

        /// <summary>
        /// 改札端末
        /// </summary>
        [LocalizedDescription("FareGateTerminal", typeof(Resources.TerminalType))]
        FareGateTerminal = 26,

        /// <summary>
        /// 携帯電話 / けいたいでんわ
        /// </summary>
        [LocalizedDescription("MobilePhone", typeof(Resources.TerminalType))]
        MobilePhone = 27,

        /// <summary>
        /// 乗継精算機 / のりつぎせいさんき
        /// </summary>
        [LocalizedDescription("TransferFareAdjustmentMachine", typeof(Resources.TerminalType))]
        TransferFareAdjustmentMachine = 28,

        /// <summary>
        /// 連絡改札機 / れんらくかいさつき
        /// </summary>
        [LocalizedDescription("ConnectingFareGate", typeof(Resources.TerminalType))]
        ConnectingFareGate = 29,

        /// <summary>
        /// 簡易入金機
        /// </summary>
        [LocalizedDescription("EasyDepositMachine", typeof(Resources.TerminalType))]
        EasyDepositMachine = 31,

        /// <summary>
        /// VIEW ALTTE
        /// </summary>
        /// <seealso href="https://www.jreast.co.jp/card/guide/atm/"/>
        [LocalizedDescription("ViewAltteA", typeof(Resources.TerminalType))]
        ViewAltteA = 70,

        /// <summary>
        /// VIEW ALTTE
        /// </summary>
        /// <seealso href="https://www.jreast.co.jp/card/guide/atm/"/>
        [LocalizedDescription("ViewAltteB", typeof(Resources.TerminalType))]
        ViewAltteB = 72,

        /// <summary>
        /// 物販端末
        /// </summary>
        [LocalizedDescription("PointOfSale", typeof(Resources.TerminalType))]
        PointOfSale = 199,

        /// <summary>
        /// 自販機 / じはんき
        /// </summary>
        [LocalizedDescription("VendingMachine", typeof(Resources.TerminalType))]
        VendingMachine = 200
    }
}

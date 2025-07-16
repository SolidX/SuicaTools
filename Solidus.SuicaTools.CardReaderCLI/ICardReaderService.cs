
namespace Solidus.SuicaTools.CardReaderCLI
{
    internal interface ICardReaderService
    {
        Task<bool> InitializeCardReader();
    }
}

using Hazzik.Qif;
using Hazzik.Qif.Transactions;
using Solidus.SuicaTools.Localization;
using System.Globalization;

namespace Solidus.SuicaTools
{
    public static class ExportExtensions
    {
        /// <summary>
        /// Generates and outputs a QIF file from the given <paramref name="history"/>
        /// </summary>
        /// <param name="history">A collection of SuicaLogEntries</param>
        /// <param name="output">Stream to write QIF file to</param>
        public static async void ExportAsQIFFile(this SuicaHistory history, Stream output)
        {
            QifDocument doc = new QifDocument();
            var transactions = await history.ToQifTransactionList();

            foreach (var t in transactions)
                doc.CashTransactions.Add(t);

            doc.Save(output);
        }

        /// <summary>
        /// Generates and outputs a QIF file from the given <paramref name="history"/>
        /// </summary>
        /// <param name="history">A collection of SuicaLogEntries</param>
        /// <param name="output">TextWriter to write QIF file to</param>
        public static async void ExportAsQIFFile(this SuicaHistory history, TextWriter output)
        {
            QifDocument doc = new QifDocument();
            var transactions = await history.ToQifTransactionList();

            foreach (var t in transactions)
                doc.CashTransactions.Add(t);

            doc.Save(output);
        }

        /// <summary>
        /// Converts a SuicaHistory object in to a List of QIF Transactions
        /// </summary>
        /// <param name="history">The card history to convert.</param>
        /// <returns>A list of QIF transactions</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="history"/> is null</exception>
        private static async Task<IEnumerable<BasicTransaction>> ToQifTransactionList(this SuicaHistory history)
        {
            if (history == null)      throw new ArgumentNullException(nameof(history));
            if (history.Count == 0)   return Enumerable.Empty<BasicTransaction>();

            var transactionList = new List<BasicTransaction>();
            foreach (var e in history)
            {
                var transaction = new BasicTransaction
                {
                    Date = e.GetTimestampUtc(),
                    Number = e.TransactionNumber.ToString(),
                    ClearedStatus = "X",
                    //TODO: If more data becomes available, implement setting transaction.Payee
                    Memo = await e.ToQifMemoString(CultureInfo.CurrentCulture),
                    Amount = history.GetTransactionAmount(e.TransactionNumber) ?? 0
                };

                transactionList.Add(transaction);
            }

            return transactionList;
        }

        /// <summary>
        /// Generates a QIF memo string from a given SuicaLogEntry
        /// </summary>
        /// <param name="entry">The provided transaction</param>
        /// <param name="locale">If set, forces locale of generated memo string otherwise defaults to the locale of the current UI</param>
        /// <returns>A brief description of a given <paramref name="entry"/></returns>
        private static async Task<string> ToQifMemoString(this SuicaLogEntry entry, CultureInfo? locale)
        {
            //TODO: Needs localization (currently only returns what we've got preferring english)
            if (entry.IsBusRelated())
            {
                var stopInfo = entry.GetBusStop();

                if (stopInfo != null)
                    return $"Bus {stopInfo.NativeOperatorName} - {stopInfo.NativeStationName}";
                return "Bus";
            }

            if (entry.IsASaleOfGoods())
                return "Purchase";

            var trainEntry = await entry.GetEntryStation();
            var trainExit = await entry.GetExitStation();

            var converter = new EnumLocalizedDescriptionTypeConverter(entry.TransactionType.GetType());
            var transactionType = converter.ConvertTo(null, locale, entry.TransactionType, typeof(string));

            if (trainEntry != null)
            {
                if (trainExit != null)
                {
                    return $"Train - Entry: {trainEntry.LocalizedStationName ?? trainEntry.NativeStationName} Exit: {trainExit.LocalizedStationName ?? trainExit.NativeStationName}";
                }
                return $"Train {transactionType}: {trainEntry.LocalizedStationName ?? trainEntry.NativeStationName}";
            }
            return $"{transactionType}";
        }
    }
}

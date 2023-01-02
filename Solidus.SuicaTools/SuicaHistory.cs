using Hazzik.Qif;
using System.Collections;

namespace Solidus.SuicaTools
{
    /// <summary>
    /// Represents the entire Suica History log.
    /// </summary>
    public class SuicaHistory : ICollection<SuicaLogEntry>
    {
        private readonly Dictionary<uint, SuicaLogEntry> _entries;

        public int Count => _entries.Count;

        public bool IsReadOnly => false;

        public SuicaHistory()
        {
            _entries = new Dictionary<uint, SuicaLogEntry>();
        }

        public SuicaHistory(IEnumerable<SuicaLogEntry> entries)
        {
            _entries = entries.ToDictionary(k => k.TransactionNumber);
        }

        /// <summary>
        /// Checks to see if the history contains a transaction with the specified transaction number.
        /// </summary>
        /// <param name="transactionNumber">The transaction number to look up.</param>
        /// <returns>True if has a SuicaLogEntry with the provided <paramref name="transactionNumber"/>, false otherwise.</returns>
        public bool Contains(uint transactionNumber)
        {
            return _entries.ContainsKey(transactionNumber);
        }

        /// <summary>
        /// Adds a transaction to the history.
        /// </summary>
        /// <param name="entry">The transaction to add.</param>
        /// <exception cref="ArgumentException">If <paramref name="entry"/> has the same Transaction Number as another History entry.</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="entry"/> is null.</exception>
        public void Add(SuicaLogEntry entry)
        {
            _entries.Add(entry.TransactionNumber, entry);
        }

        /// <summary>
        /// Gets a particular transaction record.
        /// </summary>
        /// <param name="transactionNumber">The transaction number to look up.</param>
        /// <returns>The requested transaction or null if it cannot be found</returns>
        public SuicaLogEntry? Get(uint transactionNumber)
        {
            if (_entries.ContainsKey(transactionNumber))
                return _entries[transactionNumber];
            return null;
        }

        // <summary>
        /// Gets a particular transaction record.
        /// </summary>
        /// <param name="transactionNumber">The transaction number to look up.</param>
        /// <returns>The requested transaction or null if it cannot be found</return
        public SuicaLogEntry? this[uint transactionNumber]
        {
            get { return Get(transactionNumber); }
        }

        /// <summary>
        /// Determines the value of a particular transaction.
        /// </summary><
        /// <param name="transactionNumber">The transaction number to look up.</param>
        /// <returns>The difference in balance between this transaction and the one that immediately preceeds it or null if the amount can't be determined because there's a gap in the transaction history.</returns>
        /// <exception cref="KeyNotFoundException">If the provided <paramref name="transactionNumber"/> is not available in the history.</exception>
        public uint? GetTransactionAmount(uint transactionNumber)
        {
            if (_entries.ContainsKey(transactionNumber))
            {
                uint? prevTranNum = null;

                if (_entries.ContainsKey(transactionNumber - 1))                            prevTranNum = transactionNumber - 1;
                if (!prevTranNum.HasValue && _entries.ContainsKey(transactionNumber - 2))   prevTranNum = transactionNumber - 2;

                if (prevTranNum.HasValue)
                {
                    return (uint?)Math.Abs(Convert.ToInt64(_entries[prevTranNum.Value].Balance) - Convert.ToInt64(_entries[transactionNumber].Balance));
                }
                return null;
            }
            throw new KeyNotFoundException("Transaction number not found.");
        }

        public void Clear()
        {
            _entries.Clear();
        }

        public bool Contains(SuicaLogEntry item)
        {
            return _entries.Remove(item.TransactionNumber);
        }

        public void CopyTo(SuicaLogEntry[] array, int arrayIndex)
        {
            var i = arrayIndex;
            foreach (var e in this)
            {
                array[i] = e;
                i++;
            }
        }

        public bool Remove(SuicaLogEntry item)
        {
            return _entries.Remove(item.TransactionNumber);
        }

        public IEnumerator<SuicaLogEntry> GetEnumerator()
        {
            return _entries.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

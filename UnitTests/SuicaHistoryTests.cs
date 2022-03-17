using Bogus;
using Microsoft.EntityFrameworkCore;
using Solidus.SuicaTools;
using Solidus.SuicaTools.Data;
using System.Runtime.CompilerServices;
using Xunit;

namespace UnitTests
{
    public class SuicaHistoryTests
    {
        private Faker _f = new Faker();

        public static TransitContext GetContext([CallerMemberName] string methodName = "UnitTest")
        {
            var options = new DbContextOptionsBuilder<TransitContext>().UseInMemoryDatabase(methodName).Options;
            return new TransitContext(options);
        }

        [Fact]
        public void ShouldAddTransaction()
        {
            var context = GetContext();
            var data = new byte[] { 0x16, 0x01, 0x00, 0x02, 0x28, 0x50, 0x25, 0x0C, 0x03, 0x0D, 0x70, 0x01, 0x00, 0x00, 0xA2, 0x00 };

            var transaction = new SuicaLogEntry(data, context);
            var history = new SuicaHistory();
            
            Assert.Equal(0, history.Count);
            history.Add(transaction);
            Assert.Equal(1, history.Count);
            Assert.True(history.Contains(162));
        }

        [Fact]
        public void ShouldGetExistingTransaction()
        {
            var context = GetContext();
            var data = new byte[] { 0x16, 0x01, 0x00, 0x02, 0x28, 0x50, 0x25, 0x0C, 0x03, 0x0D, 0x70, 0x01, 0x00, 0x00, 0xA2, 0x00 };

            var transactionId = 162u;
            var transaction = new SuicaLogEntry(data, context);
            var history = new SuicaHistory();
            history.Add(transaction);
            
            Assert.True(history.Contains(transactionId));
            Assert.NotNull(history[transactionId]);
            Assert.NotNull(history.Get(transactionId));
        }

        [Fact]
        public void ShouldNotGetInvalidTransaction()
        {
            var history = new SuicaHistory();
            var invalidId = _f.Random.UInt();

            Assert.Null(history[invalidId]);
            Assert.Null(history.Get(invalidId));
        }

        [Fact]
        public void ShouldGetTransactionAmount()
        {
            var context = GetContext();
            var tran162 = new byte[] { 0x16, 0x01, 0x00, 0x02, 0x28, 0x50, 0x25, 0x0C, 0x03, 0x0D, 0x70, 0x01, 0x00, 0x00, 0xA2, 0x00 };
            var tran160 = new byte[] { 0xC7, 0x46, 0x00, 0x00, 0x28, 0x50, 0x5B, 0xEF, 0x07, 0x7B, 0xF8, 0x01, 0x00, 0x00, 0xA0, 0x00 };
            var tran159 = new byte[] { 0xC8, 0x46, 0x00, 0x00, 0x28, 0x50, 0x05, 0xEF, 0x4A, 0x72, 0xCF, 0x04, 0x00, 0x00, 0x9F, 0x00 };

            var historyA = new SuicaHistory(new SuicaLogEntry[] { new SuicaLogEntry(tran162, context), new SuicaLogEntry(tran160, context) });
            var historyB = new SuicaHistory(new SuicaLogEntry[] { new SuicaLogEntry(tran160, context), new SuicaLogEntry(tran159, context) });

            var amount162 = historyA.GetTransactionAmount(162u);
            var amount160 = historyB.GetTransactionAmount(160u);

            Assert.NotNull(amount162);
            Assert.Equal(136u, amount162);
            Assert.NotNull(amount160);
            Assert.Equal(727u, amount160);
        }

        [Fact]
        public void ShouldNotGetTransactionAmount()
        {
            var context = GetContext();
            var data = new byte[] { 0x16, 0x01, 0x00, 0x02, 0x28, 0x50, 0x25, 0x0C, 0x03, 0x0D, 0x70, 0x01, 0x00, 0x00, 0xA2, 0x00 };

            var transactionId = 162u;
            var transaction = new SuicaLogEntry(data, context);
            var history = new SuicaHistory();
            history.Add(transaction);

            Assert.Null(history.GetTransactionAmount(transactionId));
        }
    }
}

using Bogus;
using Microsoft.EntityFrameworkCore;
using Solidus.SuicaTools;
using Solidus.SuicaTools.Data;
using Solidus.SuicaTools.Data.Entities.EkiData;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Xunit;

namespace UnitTests
{
    public class ExportExtensionTests
    {
        private readonly Faker _f = new Faker();

        public static TransitContext GetContext([CallerMemberName] string methodName = "UnitTest")
        {
            var options = new DbContextOptionsBuilder<TransitContext>().UseInMemoryDatabase(methodName).Options;
            return new TransitContext(options);
        }

        [Fact]
        public void ShouldExportAsQif()
        {
            //TODO: Eventually write proper export tests instead of being lazy

            var ctx = GetContext();

            var regionCode = (byte)0x00;
            var entryLineCode = (byte)0x25;
            var entryStationCode = (byte)0x0C;
            var exitLineCode = (byte)0x03;
            var exitStationCode = (byte)0x0D;

            var entrySaibane = new SaibaneCode
            {
                RegionCode = regionCode,
                LineCode = entryLineCode,
                StationCode = entryStationCode,
                OperatorName = "JR East",
                LineName = "Chuo Main Line",
                StationName = "Shin-Okubo"
            };

            var exitSaibane = new SaibaneCode
            {
                RegionCode = regionCode,
                LineCode = exitLineCode,
                StationCode = exitStationCode,
                OperatorName = "JR East",
                LineName = "Yamanote Line",
                StationName = "Shinjuku"
            };

            ctx.Add(entrySaibane);
            ctx.Add(exitSaibane);
            ctx.SaveChanges();

            var history = new SuicaHistory
            {
                new SuicaLogEntry(new byte[] { 0x16, 0x01, 0x00, 0x02, 0x28, 0x50, entryLineCode, entryStationCode, exitLineCode, exitStationCode, 0x70, 0x01, 0x00, 0x00, 0xA2, regionCode }, ctx), // No. 162 
                new SuicaLogEntry(new byte[] { 0xC7, 0x46, 0x00, 0x00, 0x28, 0x50, 0x5B, 0xEF, 0x07, 0x7B, 0xF8, 0x01, 0x00, 0x00, 0xA0, 0x00 }, ctx), // No. 160
                new SuicaLogEntry(new byte[] { 0xC8, 0x46, 0x00, 0x00, 0x28, 0x50, 0x05, 0xEF, 0x4A, 0x72, 0xCF, 0x04, 0x00, 0x00, 0x9F, 0x00 }, ctx), // No. 159
            };

            using (var memory = new MemoryStream())
            {
                history.ExportAsQIFFile(memory);
                var str = Encoding.UTF8.GetString(memory.ToArray());
                Assert.NotNull(str);
                Assert.Contains(entrySaibane.StationName, str);
                Assert.Contains(exitSaibane.StationName, str);
            }            
        }
    }
}

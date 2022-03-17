using Bogus;
using Microsoft.EntityFrameworkCore;
using Solidus.SuicaTools;
using Solidus.SuicaTools.Data;
using Solidus.SuicaTools.Data.Entities.EkiData;
using System;
using System.Runtime.CompilerServices;
using Xunit;

namespace UnitTests
{
    public class SuicaLogEntryTests
    {
        private Faker _f = new Faker();

        public static TransitContext GetContext([CallerMemberName] string methodName = "UnitTest")
        {
            var options = new DbContextOptionsBuilder<TransitContext>().UseInMemoryDatabase(methodName).Options;
            return new TransitContext(options);
        }

        [Fact]
        public void ShouldThrowExceptionOnInvalidDataLength()
        {
            var context = GetContext();
            var tooShort = new byte[] { 0x00 };
            var tooLong = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01 };

            Assert.Throws<ArgumentException>(() => new SuicaLogEntry(tooShort, context));
            Assert.Throws<ArgumentException>(() => new SuicaLogEntry(tooLong, context));
        }

        [Fact]
        public void ShouldGetTerminalType()
        {
            var context = GetContext();
            var expectedValue = _f.PickRandom<TerminalType>();
            var data = new byte[] { (byte)expectedValue, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };


            var transaction = new SuicaLogEntry(data, context);
            Assert.Equal(expectedValue, transaction.TerminalType);
        }

        [Fact]
        public void ShouldGetTransactionType()
        {
            var context = GetContext();
            var expectedValue = _f.PickRandom<TransactionType>();
            var data = new byte[] { 0x00, (byte)expectedValue, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

            var transaction = new SuicaLogEntry(data, context);
            Assert.Equal(expectedValue, transaction.TransactionType);
        }

        [Fact]
        public void ShouldGetPaymentMethod()
        {
            var context = GetContext();
            var expectedValue = _f.PickRandom<PaymentType>();
            var data = new byte[] { 0x00, 0x00, (byte)expectedValue, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

            var transaction = new SuicaLogEntry(data, context);
            Assert.Equal(expectedValue, transaction.PaymentMethod);
        }

        [Fact]
        public void ShouldGetEntryExitType()
        {
            var context = GetContext();
            var expectedValue = _f.PickRandom<EntryExitType>();
            var data = new byte[] { 0x00, 0x00, 0x00, (byte)expectedValue, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

            var transaction = new SuicaLogEntry(data, context);
            Assert.Equal(expectedValue, transaction.EntryExitType);
        }

        [Fact]
        public void ShouldGetTransactionDate()
        {
            var context = GetContext();
            // 03/18/2007
            var data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x0E, 0x72, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            var date = new DateOnly(2007, 03, 18);

            var transaction = new SuicaLogEntry(data, context);
            Assert.Equal(date, transaction.TransactionDate);
        }

        [Fact]
        public void ShouldHaveBalance()
        {
            var context = GetContext();
            var data = new byte[] { 0x16, 0x01, 0x00, 0x02, 0x28, 0x50, 0x25, 0x0C, 0x03, 0x0D, 0x70, 0x01, 0x00, 0x00, 0xA2, 0x00 };
            var expectedBalance = 368u;

            var transaction = new SuicaLogEntry(data, context);
            Assert.Equal(expectedBalance, transaction.Balance);
        }

        [Fact]
        public void ShouldHaveTransactionNumber()
        {
            var context = GetContext();
            var data = new byte[] { 0x16, 0x01, 0x00, 0x02, 0x28, 0x50, 0x25, 0x0C, 0x03, 0x0D, 0x70, 0x01, 0x00, 0x00, 0xA2, 0x00 };
            var expectedNumber = 162u;

            var transaction = new SuicaLogEntry(data, context);
            Assert.Equal(expectedNumber, transaction.TransactionNumber);
        }

        [Fact]
        public void ShouldHaveRegionCode()
        {
            var context = GetContext();
            var data = new byte[] { 0x16, 0x01, 0x00, 0x02, 0x28, 0x4B, 0x81, 0x24, 0x81, 0x1C, 0x02, 0x01, 0x00, 0x00, 0x8C, 0xA0 };

            var transaction = new SuicaLogEntry(data, context);
            Assert.Equal(RegionCode.Kansai, transaction.RegionCode);
        }

        #region Train Transaction Tests
        [Fact]
        public void TrainsShouldHaveTrainLineAndStationInfo()
        {
            var context = GetContext();
            var data = new byte[] { 0x16, 0x01, 0x00, 0x02, 0x28, 0x50, 0x25, 0x0C, 0x03, 0x0D, 0x70, 0x01, 0x00, 0x00, 0xA2, 0x00 };

            var transaction = new SuicaLogEntry(data, context);
            Assert.False(transaction.IsBusRelated());
            Assert.False(transaction.IsASaleOfGoods());
            Assert.NotNull(transaction.EntryLineCode);
            Assert.NotNull(transaction.EntryStationCode);
            Assert.NotNull(transaction.ExitLineCode);
            Assert.NotNull(transaction.ExitStationCode);
        }

        [Fact]
        public void TrainsShouldGetTransactionTimestampUtc()
        {
            var context = GetContext();
            var data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x0E, 0x72, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

            var transaction = new SuicaLogEntry(data, context);
            var timestamp = transaction.GetTimestampUtc();

            Assert.False(transaction.IsBusRelated());
            Assert.False(transaction.IsASaleOfGoods());
            Assert.Equal(transaction.TransactionDate.Year, timestamp.Year);
            Assert.Equal(transaction.TransactionDate.Month, timestamp.Month);
            Assert.Equal(transaction.TransactionDate.Day, timestamp.Day);
            Assert.Equal(0, timestamp.Hour);
            Assert.Equal(0, timestamp.Minute);
            Assert.Equal(0, timestamp.Second);
        }

        [Fact]
        public void TrainsShouldNotLookupBusStopInfo()
        {
            var context = GetContext();
            var data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x0E, 0x72, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

            var transaction = new SuicaLogEntry(data, context);
            Assert.False(transaction.IsBusRelated());
            Assert.False(transaction.IsASaleOfGoods());
            Assert.Null(transaction.GetBusStop());
        }

        [Fact]
        public async void TrainsShouldLookupStationInfo()
        {
            var context = GetContext();
            
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

            context.Add(entrySaibane);
            context.Add(exitSaibane);
            context.SaveChanges();

            var data = new byte[] { 0x16, 0x01, 0x00, 0x02, 0x28, 0x50, entryLineCode, entryStationCode, exitLineCode, exitStationCode, 0x70, 0x01, 0x00, 0x00, 0xA2, regionCode };
            var transaction = new SuicaLogEntry(data, context);
            var entryStationInfo = await transaction.GetEntryStation();
            var exitStationInfo = await transaction.GetExitStation();

            Assert.False(transaction.IsBusRelated());
            Assert.False(transaction.IsASaleOfGoods());
            Assert.Equal(entrySaibane.OperatorName, entryStationInfo.NativeOperatorName);
            Assert.Equal(entrySaibane.LineName, entryStationInfo.NativeLineName);
            Assert.Equal(entrySaibane.StationName, entryStationInfo.NativeStationName);
            Assert.Equal(exitSaibane.OperatorName, exitStationInfo.NativeOperatorName);
            Assert.Equal(exitSaibane.LineName, exitStationInfo.NativeLineName);
            Assert.Equal(exitSaibane.StationName, exitStationInfo.NativeStationName);
        }
        #endregion

        #region Point of Sale Transaction Tests
        [Fact]
        public void PurchasesShouldNotHaveTrainLineAndStationInfo()
        {
            var context = GetContext();
            var data = new byte[] { 0xC8, 0x46, 0x00, 0x00, 0x28, 0x50, 0x01, 0xAF, 0x4A, 0xD2, 0xC3, 0x06, 0x00, 0x00, 0x9A, 0x00 };

            var transaction = new SuicaLogEntry(data, context);
            Assert.True(transaction.IsASaleOfGoods());
            Assert.Null(transaction.EntryLineCode);
            Assert.Null(transaction.EntryStationCode);
            Assert.Null(transaction.ExitLineCode);
            Assert.Null(transaction.ExitStationCode);
        }

        [Fact]
        public void PurchasesShouldHaveTransactionTimeAndPointOfSaleId()
        {
            var context = GetContext();
            var data = new byte[] { 0xC8, 0x46, 0x00, 0x00, 0x28, 0x50, 0x01, 0xAF, 0x4A, 0xD2, 0xC3, 0x06, 0x00, 0x00, 0x9A, 0x00 };

            var transaction = new SuicaLogEntry(data, context);
            Assert.True(transaction.IsASaleOfGoods());
            Assert.NotNull(transaction.TransactionTime);
            Assert.NotNull(transaction.PointOfSaleId);
        }

        [Fact]
        public void PurchasesShouldGetTransactionTimestampUtc()
        {
            var context = GetContext();
            var data = new byte[] { 0xC8, 0x46, 0x00, 0x00, 0x28, 0x50, 0x01, 0xAF, 0x4A, 0xD2, 0xC3, 0x06, 0x00, 0x00, 0x9A, 0x00 };
            var expectedTimeJst = new DateTime(2020, 02, 16, 0, 13, 30, DateTimeKind.Unspecified);
            var expectedTimeUtc = TimeZoneInfo.ConvertTimeToUtc(expectedTimeJst, TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time"));

            var transaction = new SuicaLogEntry(data, context);
            Assert.True(transaction.IsASaleOfGoods());
            Assert.NotNull(transaction.TransactionTime);
            Assert.Equal(expectedTimeUtc, transaction.GetTimestampUtc());
        }

        [Fact]
        public void PurchasesShouldNotLookupBusStopInfo()
        {
            var context = GetContext();
            var data = new byte[] { 0xC8, 0x46, 0x00, 0x00, 0x28, 0x50, 0x01, 0xAF, 0x4A, 0xD2, 0xC3, 0x06, 0x00, 0x00, 0x9A, 0x00 };

            var transaction = new SuicaLogEntry(data, context);
            Assert.True(transaction.IsASaleOfGoods());
            Assert.Null(transaction.GetBusStop());
        }

        [Fact]
        public async void PurchasesShouldNotLookupTrainStationInfo()
        {
            var context = GetContext();
            var data = new byte[] { 0xC8, 0x46, 0x00, 0x00, 0x28, 0x50, 0x01, 0xAF, 0x4A, 0xD2, 0xC3, 0x06, 0x00, 0x00, 0x9A, 0x00 };

            var transaction = new SuicaLogEntry(data, context);
            Assert.True(transaction.IsASaleOfGoods());
            Assert.Null(await transaction.GetEntryStation());
            Assert.Null(await transaction.GetExitStation());
        }
        #endregion

        #region Bus/Tram Transaction Tests
        [Fact]
        public void BusesShouldNotHaveTrainLineAndStationInfo()
        {
            var context = GetContext();
            var data = new byte[] { 0x05, 0x0D, 0x00, 0x0F, 0x0E, 0xA7, 0x0C, 0x4A, 0x00, 0x00, 0xFA, 0x0F, 0x00, 0x00, 0xB3, 0x00 };

            var transaction = new SuicaLogEntry(data, context);
            Assert.True(transaction.IsBusRelated());
            Assert.Null(transaction.EntryLineCode);
            Assert.Null(transaction.EntryStationCode);
            Assert.Null(transaction.ExitLineCode);
            Assert.Null(transaction.ExitStationCode);
        }

        [Fact]
        public void BusesShouldHaveBusInfo()
        {
            var context = GetContext();
            var data = new byte[] { 0x05, 0x0D, 0x00, 0x0F, 0x0E, 0xA7, 0x0C, 0x4A, 0x00, 0x00, 0xFA, 0x0F, 0x00, 0x00, 0xB3, 0x00 };

            var transaction = new SuicaLogEntry(data, context);
            Assert.True(transaction.IsBusRelated());
            Assert.NotNull(transaction.BusOperatorCode);
            Assert.NotNull(transaction.BusStopCode);
        }

        [Fact]
        public void BusesShouldNotHaveTransactionTimeOrPointOfSaleId()
        {
            var context = GetContext();
            var data = new byte[] { 0x05, 0x0D, 0x00, 0x0F, 0x0E, 0xA7, 0x0C, 0x4A, 0x00, 0x00, 0xFA, 0x0F, 0x00, 0x00, 0xB3, 0x00 };

            var transaction = new SuicaLogEntry(data, context);
            Assert.True(transaction.IsBusRelated());
            Assert.Null(transaction.TransactionTime);
            Assert.Null(transaction.PointOfSaleId);
        }

        [Fact]
        public void BusesShouldGetTransactionTimestampUtc()
        {
            var context = GetContext();
            var data = new byte[] { 0x05, 0x0D, 0x00, 0x0F, 0x0E, 0xA7, 0x0C, 0x4A, 0x00, 0x00, 0xFA, 0x0F, 0x00, 0x00, 0xB3, 0x00 };

            var transaction = new SuicaLogEntry(data, context);
            var timestamp = transaction.GetTimestampUtc();

            Assert.True(transaction.IsBusRelated());
            Assert.Equal(transaction.TransactionDate.Year, timestamp.Year);
            Assert.Equal(transaction.TransactionDate.Month, timestamp.Month);
            Assert.Equal(transaction.TransactionDate.Day, timestamp.Day);
            Assert.Equal(0, timestamp.Hour);
            Assert.Equal(0, timestamp.Minute);
            Assert.Equal(0, timestamp.Second);
        }

        [Fact]
        public void BusesShouldLookupBusStopInfo()
        {
            var context = GetContext();
            var data = new byte[] { 0x05, 0x0F, 0x00, 0x0F, 0x0D, 0x9D, 0x0E, 0x33, 0xD2, 0xB7, 0xFD, 0x0A, 0x00, 0x00, 0x35, 0xA0 };

            var stop = new IruCaBusStop
            {
                Id = _f.Random.Int(0),
                LineCode = 0x0E33,
                StationCode = 0xD2B7,
                OperatorName = "Shinki Bus",
                LineName = "",
                StationName= "Sakae Ekimae"                
            };
            context.IruCaBusStops.Add(stop);
            context.SaveChanges();

            var transaction = new SuicaLogEntry(data, context);
            var busStopInfo = transaction.GetBusStop();

            Assert.True(transaction.IsBusRelated());
            Assert.NotNull(busStopInfo);
            Assert.Equal(stop.LineCode, busStopInfo.LineCode);
            Assert.Equal(stop.StationCode, busStopInfo.StopCode);
            Assert.Equal(stop.OperatorName, busStopInfo.NativeOperatorName);
            Assert.Equal(stop.LineName, busStopInfo.NativeLineName);
            Assert.Equal(stop.StationName, busStopInfo.NativeStationName);
        }

        [Fact]
        public async void BusesShouldNotLookupTrainStationInfo()
        {
            var context = GetContext();
            var data = new byte[] { 0x05, 0x0D, 0x00, 0x0F, 0x0E, 0xA7, 0x0C, 0x4A, 0x00, 0x00, 0xFA, 0x0F, 0x00, 0x00, 0xB3, 0x00 };

            var transaction = new SuicaLogEntry(data, context);
            Assert.True(transaction.IsBusRelated());
            Assert.Null(await transaction.GetEntryStation());
            Assert.Null(await transaction.GetExitStation());
        }
        #endregion
    }
}

using Solidus.SuicaTools;
using Solidus.SuicaTools.Localization;
using System.Threading;
using Xunit;

namespace UnitTests
{
    public class EnumLocalizationTests
    {
        [Fact]
        public void EnumsShouldLocalizeDescriptionsBasedOnUICulture()
        {
            var enumValue = RegionCode.GreaterTokyoArea;
            var converter = new EnumLocalizedDescriptionTypeConverter(enumValue.GetType());

            //Default to default locale when using InvariantCulture
            Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.InvariantCulture;
            Assert.Equal("Greater Tokyo Area", converter.ConvertTo(null, null, enumValue, typeof(string)));

            //Default locale is en so this should work for en-US
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            Assert.Equal("Greater Tokyo Area", converter.ConvertTo(null, null, enumValue, typeof(string)));

            //Localized in jp so this should work
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ja");
            Assert.Equal("首都圏", converter.ConvertTo(null, null, enumValue, typeof(string)));
            
            //Default to default locale when specifying an unhandled locale
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("fr-FR");
            Assert.Equal("Greater Tokyo Area", converter.ConvertTo(null, null, enumValue, typeof(string)));
        }

        [Fact]
        public void ShouldLocalizeDescriptionsBasedOnConverterParams()
        {
            var enumValue = RegionCode.GreaterTokyoArea;
            var converter = new EnumLocalizedDescriptionTypeConverter(enumValue.GetType());

            //Default to default locale when using InvariantCulture
            Assert.Equal("Greater Tokyo Area", converter.ConvertTo(null, System.Globalization.CultureInfo.InvariantCulture, enumValue, typeof(string)));

            //Default locale is en so this should work for en-US
            Assert.Equal("Greater Tokyo Area", converter.ConvertTo(null, new System.Globalization.CultureInfo("en-US"), enumValue, typeof(string)));

            //Localized in jp so this should work
            Assert.Equal("首都圏", converter.ConvertTo(null, new System.Globalization.CultureInfo("ja"), enumValue, typeof(string)));

            //Default to default locale when specifying an unhandled locale
            Assert.Equal("Greater Tokyo Area", converter.ConvertTo(null, new System.Globalization.CultureInfo("fr-FR"), enumValue, typeof(string)));
        }
    }
}

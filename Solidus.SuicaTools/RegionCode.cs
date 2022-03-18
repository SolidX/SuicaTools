using Solidus.SuicaTools.Localization;
using System.ComponentModel;

namespace Solidus.SuicaTools
{
    /// <see href="http://jennychan.web.fc2.com/format/suica.html"/>
    [TypeConverter(typeof(EnumLocalizedDescriptionTypeConverter))]
    public enum RegionCode
    {
        /// <summary>
        /// Region Code is unknown or invalid
        /// </summary>
        /// <remarks>This isn't actually part of the spec.</remarks>
        [LocalizedDescription("Unknown", typeof(Resources.RegionCode))]
        Unknown = 0xFFF,

        /// <summary>
        /// The Greater Tokyo Area or Kanto
        /// </summary>
        /// <seealso href="https://jisho.org/word/518698a2d5dda7b2c60536f1"/>
        [LocalizedDescription("GreaterTokyoArea", typeof(Resources.RegionCode))]
        GreaterTokyoArea = 0x00,

        /// <summary>
        /// Appears to only contain Lines & Stations in Aichi and Shizuoka Prefectures... Chubu maybe?
        /// </summary>
        /// <remarks>This is my guess since the author doesn't know what this is either.</remarks>
        [LocalizedDescription("Chubu", typeof(Resources.RegionCode))]
        Chubu = 0x01,

        /// <summary>
        /// Kansai
        /// </summary>
        [LocalizedDescription("Kansai", typeof(Resources.RegionCode))]
        Kansai = 0x02,

        /// <summary>
        /// Bascially everywhere else (mostly western Japan)
        /// </summary>
        [LocalizedDescription("OtherRegions", typeof(Resources.RegionCode))]
        OtherRegions = 0x03
    }
}

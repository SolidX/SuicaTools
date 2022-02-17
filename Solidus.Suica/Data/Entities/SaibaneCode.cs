using System;
using System.ComponentModel.DataAnnotations;

namespace Solidus.SuicaTools.Data.Entities.EkiData
{
    public class SaibaneCode
    {
        [Range(0, 255)]
        public short RegionCode { get; set; }
        [Range(0, 255)]
        public short LineCode { get; set; }
        [Range(0, 255)]
        public short StationCode { get; set; }
        public int? EkiData_StationCode { get; set; }
        public Station? Station { get; set; }
        public string? StationNameOverride_JA { get; set; }
        public string? StationNameOverride_EN { get; set; }
    }
}

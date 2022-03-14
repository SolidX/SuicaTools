namespace Solidus.SuicaTools.Data.Entities.EkiData
{
    public class SaibaneCode
    {
        public byte RegionCode { get; set; }
        public byte LineCode { get; set; }
        public byte StationCode { get; set; }
        public string OperatorName { get; set; }
        public string LineName { get; set; }
        public string StationName { get; set; }
        public string? StationName_English { get; set; }
    }
}

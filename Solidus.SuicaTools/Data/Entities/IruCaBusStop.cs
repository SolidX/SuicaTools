namespace Solidus.SuicaTools.Data.Entities.EkiData
{
    public class IruCaBusStop
    {
        public int Id { get; set; }
        public int LineCode { get; set; }
        public int StationCode { get; set; }
        public string OperatorName { get; set; }
        public string? LineName { get; set; }
        public string StationName { get; set; }
        public string? Note { get; set; }
    }
}

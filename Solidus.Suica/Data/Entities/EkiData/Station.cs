namespace Solidus.SuicaTools.Data.Entities.EkiData
{
    public class Station
    {
        public int StationCode { get; set; }
        public int StationGroupCode { get; set; }
        public string Name { get; set; }
        public string? Name_English { get; set; }
        public int LineCode { get; set; }
        public Line Line { get; set; }
        public byte? PrefectureId { get; set; } 
        public Prefecture? Prefecture { get; set; }
        public string? PostalCode { get; set; }
        public string? Address { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public DateTime? OpeningDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public byte? StatusId { get; set; }
        public Status? Status { get; set; }
    }
}

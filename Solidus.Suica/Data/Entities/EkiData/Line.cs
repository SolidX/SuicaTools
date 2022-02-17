using System.ComponentModel.DataAnnotations;

namespace Solidus.SuicaTools.Data.Entities.EkiData
{
    public class Line
    {
        public int LineCode { get; set; }
        public int CompanyCode { get; set; }
        public Company Company { get; set; }
        public string Name { get; set; }
        public string? Name_Katakana { get; set; }
        public string? Name_English { get; set; }
        public string? OfficialName { get; set; }
        public string? OfficialName_English { get; set; }
        
        [MaxLength(6)]
        public string? ColorCode { get; set; }
        public string? ColorName { get; set; }
        public short? LineTypeId { get; set; }
        public LineType? Type { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public short? GoogleMapZoomLevel { get; set; }
        public short? StatusId { get; set; }
        public Status? Status { get; set; }
    }
}

namespace Solidus.SuicaTools.Data.Entities.EkiData
{
    public class Company
    {
        public int CompanyCode { get; set; }
        public int RailroadCode { get; set; }
        public string Name { get; set; }
        public string? Name_Katakana { get; set; }
        public string? Name_English { get; set; }
        public string? OfficialName { get; set; }
        public string? ShortName { get; set; }
        public string? Website { get; set; }
        public short? CompanyTypeId { get; set; }
        public CompanyType? Type { get; set; }
        public short? StatusId { get; set; }
        public Status? Status { get; set; }
    }
}

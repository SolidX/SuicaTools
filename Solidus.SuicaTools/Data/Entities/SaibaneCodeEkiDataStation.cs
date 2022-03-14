using Solidus.SuicaTools.Data.Entities.EkiData;

namespace Solidus.SuicaTools.Data.Entities
{
    public class SaibaneCodeEkiDataStation
    {
        public byte RegionCode { get; set; }
        public byte LineCode { get; set; }
        public byte StationCode { get; set; }
        public SaibaneCode Saibane { get; set; }
        
        public int EkiData_StationCode { get; set; }
        public Station EkiDataStation { get; set; }
    }
}

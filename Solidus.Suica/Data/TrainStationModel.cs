using Solidus.SuicaTools.Data.Entities.EkiData;

namespace Solidus.SuicaTools.Data
{
    public class TrainStationModel
    {
        public string? NativeOperatorName { get; set; }
        public string? NativeLineName { get; set; }
        public string? NativeStationName { get; set; }
        public string? LocalizedOperatorName { get; set; }
        public string? LocalizedLineName { get; set; }
        public string? LocalizedStationName { get; set; }

        public TrainStationModel()
        {
        }

        public TrainStationModel(Station? ekidataStation, SaibaneCode? saibane)
        {
            NativeOperatorName = ekidataStation?.Line?.Company?.Name ?? saibane?.OperatorName;
            NativeLineName = ekidataStation?.Line?.Name ?? saibane?.LineName;
            NativeStationName = ekidataStation?.Name ?? saibane?.StationName;
            LocalizedOperatorName = ekidataStation?.Line?.Company?.Name_English;
            LocalizedLineName = ekidataStation?.Line?.Name_English;
            LocalizedStationName = ekidataStation?.Name_English ?? saibane?.StationName_English;
        }
    }
}

using Solidus.SuicaTools.Data.Entities.EkiData;

namespace Solidus.SuicaTools.Data
{
    public class BusStopModel
    {
        public int LineCode { get; set; }
        public int StopCode { get; set; }
        public string NativeOperatorName { get; set; }
        public string? NativeLineName { get; set; }
        public string NativeStationName { get; set; }

        //TODO: Localize Bus Stop Information (eventually)

        public BusStopModel()
        {
        }

        public BusStopModel(IruCaBusStop stop)
        {
            LineCode = stop.LineCode;
            StopCode = stop.StationCode;
            NativeOperatorName = stop.OperatorName;
            NativeLineName = stop.LineName;
            NativeStationName = stop.StationName;            
        }
    }
}

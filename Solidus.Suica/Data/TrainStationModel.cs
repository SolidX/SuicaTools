using Solidus.SuicaTools.Data.Entities.EkiData;

namespace Solidus.SuicaTools.Data
{
    public class TrainStationModel
    {
        public SaibaneCode? Saibane { get; private set; }
        public Station? EkiDataInfo { get; private set; }

        public string NativeOperatorName
        {
            get { return EkiDataInfo?.Line?.Company?.Name ?? Saibane?.OperatorName; }
        }
        public string NativeLineName
        {
            get { return EkiDataInfo?.Line?.Name ?? Saibane?.LineName; }
        }
        public string NativeStationName
        {
            get { return EkiDataInfo?.StationName ?? Saibane?.StationName; }
        }

        public string LocalizedOperatorName
        {
            get { return EkiDataInfo?.Line?.Company?.Name_English; }
        }
        public string LocalizedLineName
        {
            get { return EkiDataInfo?.Line?.Name_English; }
        }

        //TODO: LocalizedStationName

        public TrainStationModel(Station? s, SaibaneCode? sc)
        {
            Saibane = sc;
            EkiDataInfo = s;
        }
    }
}

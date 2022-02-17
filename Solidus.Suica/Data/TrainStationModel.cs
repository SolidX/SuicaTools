using Solidus.SuicaTools.Data.Entities.EkiData;

namespace Solidus.SuicaTools.Data
{
    public class TrainStationModel
    {
        public Station? EkiDataInfo { get; private set; }

        private string? _nativeName;
        public string NativeName
        {
            get { return EkiDataInfo != null ? EkiDataInfo.StationName : _nativeName; }
        }

        private string? _englishName;
        public string? EnglishName
        {
            get { return _englishName; }
        }

        public TrainStationModel(Station? s = null, string? overrideName = null, string? overrideNameEn = null)
        {
            EkiDataInfo = s;
            _nativeName = overrideName;
            _englishName = overrideNameEn;
        }
    }
}

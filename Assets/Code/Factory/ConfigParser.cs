using Code.Configs;
using Code.Interfaces;
using static Code.Assistant.ActiveObjectsName;

namespace Code.Factory
{
    internal class ConfigParser : IInitialize
    {
        private ActiveObjectConfig[] _activeObjectConfigs;
        private LevelObjectConfig[] _levelObjectConfigs;
        public IActiveObject _ballConfig;
        public IActiveObject _glassConfig;
        public IActiveObject _arrowConfig;
        public ILevelObjects _levelConfig;

        public ConfigParser(Data data)
        {
            _activeObjectConfigs = data.ActiveObjectConfig;
            _levelObjectConfigs = data.LevelObjectConfig;
        }

        public void Initialize()
        {
            for (int i = 0; i < _activeObjectConfigs.Length; i++)
            {
                switch (_activeObjectConfigs[i].Name)
                {
                     case Ball:
                         _ballConfig = _activeObjectConfigs[i];
                         break;
                     case Arrow:
                         _arrowConfig = _activeObjectConfigs[i];
                         break;
                     case Glass:
                         _glassConfig = _activeObjectConfigs[i];
                         break;
                     default:
                         break;
                }

            }

            for (int i = 0; i < _levelObjectConfigs.Length; i++)
            {
                if (_levelObjectConfigs[i].LevelNumber == 1)
                {
                    _levelConfig = _levelObjectConfigs[i];
                }
            }
        }
    }
}

using Code.Assistant;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "Data", menuName = "Configs/Data", order = 0)]
    internal class Data : ScriptableObject
    {
        private string _activeObjectsConfigPath = "ActiveObjects";
        private string _levelObjectsConfigPath = "LevelObjects";

        private ActiveObjectConfig[] _activeObjectConfigs;
        private LevelObjectConfig[] _levelObjectConfigs;

        public ActiveObjectConfig[] ActiveObjectConfig
        {
            get
            {
                if (_activeObjectConfigs == null)
                {
                    _activeObjectConfigs = Extentions.LoadAll<ActiveObjectConfig>(_activeObjectsConfigPath);
                }

                return _activeObjectConfigs;
            }
        }
        
        public LevelObjectConfig[] LevelObjectConfig
        {
            get
            {
                if (_levelObjectConfigs == null)
                {
                    _levelObjectConfigs = Extentions.LoadAll<LevelObjectConfig>(_levelObjectsConfigPath);
                }

                return _levelObjectConfigs;
            }
        }
    }
}

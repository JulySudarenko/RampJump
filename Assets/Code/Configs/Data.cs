using Code.Assistant;
using Code.Models;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "Data", menuName = "Configs/Data", order = 0)]
    internal class Data : ScriptableObject
    {
        [SerializeField] private string _activeObjectsConfigPath = "ActiveObjectConfig";
        [SerializeField] private string _levelObjectsConfigPath = "LevelObjects";

        private ActiveObjectConfig _activeObjectConfigs;
        private LevelObjectConfig[] _levelObjectConfigs;

        public ActiveObjectConfig ActiveObjectConfig
        {
            get
            {
                if (_activeObjectConfigs == null)
                {
                    _activeObjectConfigs = HelperExtentions.Load<ActiveObjectConfig>(
                        "ActiveObjects/" + _activeObjectsConfigPath);
                }

                return _activeObjectConfigs;
            }
        }

        public LevelObjectConfig[] LevelObjectConfig
        {
            get
            {
                _levelObjectConfigs = HelperExtentions.LoadAll<LevelObjectConfig>(_levelObjectsConfigPath);
                return _levelObjectConfigs;
            }
        }
    }
}

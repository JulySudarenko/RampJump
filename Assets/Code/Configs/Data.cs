using Code.Assistant;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "Data", menuName = "Configs/Data", order = 0)]
    internal class Data : ScriptableObject
    {
        [SerializeField] private string _activeObjectsConfigPath = "ActiveObjectConfig";
        [SerializeField] private string _levelObjectsConfigPath = "LevelObjects";
        [SerializeField] private string _viewConfigPath = "ViewConfig";

        private LevelObjectConfig[] _levelObjectConfigs;
        private ActiveObjectConfig _activeObjectConfigs;
        private ViewConfig _viewConfig;
        private Canvas _canvas;

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

        public ViewConfig ViewConfig
        {
            get
            {
                if (_viewConfig == null)
                {
                    _viewConfig = HelperExtentions.Load<ViewConfig>("ActiveObjects/" + _viewConfigPath);
                }

                return _viewConfig;
            }
        }

        public Canvas Canvas
        {
            get
            {
                if (_canvas == null)
                {
                    _canvas = Object.FindObjectOfType<Canvas>();
                }

                return _canvas;
            }
        }
    }
}

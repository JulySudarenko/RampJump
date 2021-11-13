using System;
using Code.Configs;
using UnityEngine;
using static Code.Assistant.ActiveObjectsName;

namespace Code.UniversalFactory
{
    internal class ConfigParser
    {
        public GameObject BallObject;
        public GameObject GlassObject;
        public float BallSpeed;
        public float GlassSpeed;
        public GameObject BallStartPlace;
        public Vector3 BallStartPosition;
        public Vector3 GlassStartPosition;
        
        private readonly ActiveObjectConfig[] _activeObjectConfigs;
        private readonly LevelObjectConfig[] _levelObjectConfigs;
        private ActiveObjectConfig _ballConfig;
        private ActiveObjectConfig _glassConfig;
        private ActiveObjectConfig _arrowConfig;
        private LevelObjectConfig _levelConfig;
        
        public ConfigParser(Data data)
        {
            _activeObjectConfigs = data.ActiveObjectConfig;
            _levelObjectConfigs = data.LevelObjectConfig;
            
             Init();
        }

        private void Init()
        {
             InitLevel();
             InitObjects();
        }

        private void InitLevel()
        {
            
            for (int i = 0; i < _levelObjectConfigs.Length; i++)
            {
                if (_levelObjectConfigs[i].LevelNumber == 1)
                {
                    BallStartPlace = new ObjectInitialization(new Factory(_levelObjectConfigs[i].BallStartPlace)).Create().gameObject;
                    BallStartPosition = _levelObjectConfigs[i].BallStartPosition.position;
                    GlassStartPosition = _levelObjectConfigs[i].GlassStartPosition.position;
                    for (int j = 0; j < _levelObjectConfigs[i].LevelDetailsPrefabs.Count; j++)
                    {
                        var levelDetail = _levelObjectConfigs[i].LevelDetailsPrefabs[j];
                        var obj = new ObjectInitialization(
                            new Factory(levelDetail)).Create();
                    }
                }
            }
        }

        private void InitObjects()
        {
            for (int i = 0; i < _activeObjectConfigs.Length; i++)
            {
                switch (_activeObjectConfigs[i].Name)
                {
                    case Ball:
                        _ballConfig = _activeObjectConfigs[i];
                        BallSpeed = _ballConfig.Speed;
                        BallObject = new ObjectInitialization(new Factory(_activeObjectConfigs[i].Prefab)).Create();
                        break;
                    case Arrow:
                        break;
                    case Glass:
                        GlassSpeed = _activeObjectConfigs[i].Speed;
                        GlassObject = new ObjectInitialization(new Factory(_activeObjectConfigs[i].Prefab)).Create();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                        break;
                }
            }
        }
    }
}

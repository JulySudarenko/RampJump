using System;
using Code.Configs;
using UnityEngine;
using static Code.Assistant.ActiveObjectsName;


namespace Code.UniversalFactory
{
    internal class ActiveObjectsConfigParser
    {
        public GameObject BallObject;
        public GameObject GlassObject;
        public GameObject ArrowObject;
        public float BallSpeed;
        public float GlassSpeed;
        public float ArrowSpeed;

        private readonly ActiveObjectConfig[] _activeObjectConfigs;

        public ActiveObjectsConfigParser(Data data)
        {
            _activeObjectConfigs = data.ActiveObjectConfig;

            Init();
        }

        private void Init()
        {
            for (int i = 0; i < _activeObjectConfigs.Length; i++)
            {
                switch (_activeObjectConfigs[i].Name)
                {
                    case Ball:
                        BallSpeed = _activeObjectConfigs[i].Speed;
                        BallObject = new ObjectInitialization(new Factory(_activeObjectConfigs[i].Prefab)).Create();
                        break;
                    case Arrow:
                        ArrowSpeed = _activeObjectConfigs[i].Speed;
                        ArrowObject = new ObjectInitialization(new Factory(_activeObjectConfigs[i].Prefab)).Create();
                        break;
                    case Glass:
                        GlassSpeed = _activeObjectConfigs[i].Speed;
                        GlassObject = new ObjectInitialization(new Factory(_activeObjectConfigs[i].Prefab)).Create();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}

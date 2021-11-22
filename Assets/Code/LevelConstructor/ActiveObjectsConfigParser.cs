using System;
using Code.Configs;
using Code.Models;
using UnityEngine;
using static Code.Assistant.ActiveObjectsName;

namespace Code.UniversalFactory
{
    internal class ActiveObjectsConfigParser
    {
        public IBallModel BallModel;
        public Transform ArrowObject;
        public Transform HoleObject;

        private readonly ActiveObjectConfig[] _activeObjectConfigs;

        public ActiveObjectsConfigParser(ActiveObjectConfig[] data)
        {
            _activeObjectConfigs = data;

            Init();
        }

        private void Init()
        {
            for (int i = 0; i < _activeObjectConfigs.Length; i++)
            {
                switch (_activeObjectConfigs[i].Name)
                {
                    case Ball:
                       BallModel = new BallModel(_activeObjectConfigs[i]); 
                        break;
                    case Arrow:
                        ArrowObject = new ObjectInitialization(new Factory(_activeObjectConfigs[i].Prefab)).Create();
                        break;
                    case Glass:
                        break;
                    case Hole:
                        HoleObject = new ObjectInitialization(new Factory(_activeObjectConfigs[i].Prefab)).Create();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}

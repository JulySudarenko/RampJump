using Code.Configs;
using Code.Models;
using Code.UniversalFactory;
using UnityEngine;

namespace Code.LevelConstructor
{
    internal class ActiveObjectsConfigParser
    {
        public IBallModel BallModel { get; private set; }
        public Transform ArrowObject { get; private set; }
        public Transform HoleObject { get; private set; }
        private readonly ActiveObjectConfig _activeObjectConfigs;
        private readonly Transform _activ;

        public ActiveObjectsConfigParser(ActiveObjectConfig data)
        {
            _activeObjectConfigs = data;
            _activ = new GameObject("Activ").transform;
            Init();
        }

        private void Init()
        {
            BallModel = new BallModel(_activeObjectConfigs, _activ);
            ArrowObject = new ObjectInitialization(new Factory(_activeObjectConfigs.ArrowPrefab, _activ)).Create();
            HoleObject = new ObjectInitialization(new Factory(_activeObjectConfigs.HolePrefab, _activ)).Create();
        }
    }
}

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
        public AudioClip BallSlimeSound { get; }
        public AudioClip CoinSound { get; }
        private readonly ActiveObjectConfig _activeObjectConfigs;

        public ActiveObjectsConfigParser(ActiveObjectConfig data)
        {
            _activeObjectConfigs = data;
            BallSlimeSound = _activeObjectConfigs.SlimeSound;
            CoinSound = _activeObjectConfigs.CoinSound;

            Init();
        }

        private void Init()
        {
            BallModel = new BallModel(_activeObjectConfigs);
            ArrowObject = new ObjectInitialization(new Factory(_activeObjectConfigs.ArrowPrefab)).Create();
            HoleObject = new ObjectInitialization(new Factory(_activeObjectConfigs.HolePrefab)).Create();
        }
    }
}

using Code.Ball;
using Code.Configs;
using Code.UniversalFactory;
using UnityEngine;

namespace Code.LevelConstructor
{
    internal class ActiveObjectsConfigParser
    {
        public IBall Ball { get; private set; }
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
            Ball = new Ball.Ball(_activeObjectConfigs);
            ArrowObject = new ObjectInitialization(new Factory(_activeObjectConfigs.ArrowPrefab)).Create();
            HoleObject = new ObjectInitialization(new Factory(_activeObjectConfigs.HolePrefab)).Create();
        }
    }
}

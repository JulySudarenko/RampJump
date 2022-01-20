using System;
using Code.Ball;
using Code.Configs;
using Code.GameState;
using Code.Interfaces;
using Code.LevelConstructor;
using UnityEngine;

namespace Code.Controllers
{
    internal class LevelController : IInitialize, ICleanup, IState
    {
        public event Action<State> OnChangeState;
        public LevelComponentsList CoinsList { get; }
        public LevelComponentsList ComponentsList { get; }

        private readonly IBall _ball;
        private readonly BallLandingController _ballLandingController;
        private readonly LevelObjectsConfigParser _configParser;
        private readonly Transform _hole;
        private readonly Transform _arrow;
        private int _levelCounter = 1;

        public LevelController(LevelObjectConfig[] config, IBall ball, Transform hole, Transform arrow)
        {
            _ball = ball;
            _hole = hole;
            _arrow = arrow;
            _configParser = new LevelObjectsConfigParser(config);
            _ballLandingController = new BallLandingController(_configParser.Bottom, ball);
            CoinsList = _configParser.CoinsList;
            ComponentsList = _configParser.ComponentsList;
        }

        public void Initialize()
        {
            _configParser.InitNewLevel(_levelCounter);
            UpdateStartPositions();
            _ballLandingController.Init();
        }

        public void ChangeState(State state)
        {
            switch (state)
            {
                case State.Start:
                    break;
                case State.BallTouched:
                    break;
                case State.BallKicked:
                    _configParser.BallStartPlace.gameObject.SetActive(false);
                    break;
                case State.Victory:
                    PlayVictoryVariant();

                    break;
                case State.Defeat:

                    //RestartLevel();
                    break;
                case State.Loading:
                    UpdateStartPositions();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void PlayVictoryVariant()
        {
            _levelCounter++;
            if (_levelCounter > _configParser.TotalLevels)
            {
                _levelCounter = 1;
            }

            _configParser.InitNewLevel(_levelCounter);
            //OnChangeState?.Invoke(State.Loading);
        }

        // private void RestartLevel()
        // {
        //     OnChangeState?.Invoke(State.Loading);
        // }

        private void UpdateStartPositions()
        {
            _configParser.BallStartPlace.gameObject.SetActive(true);
            _ball.BallTransform.position = _configParser.BallStartPosition;
            _ball.BallRigidbody.velocity = Vector3.zero;
            _ball.BallRigidbody.angularVelocity = Vector3.zero;
            _arrow.position = _configParser.BallStartPosition;
            _hole.position = _configParser.HoleStartPosition;
            _configParser.ReloadCoins(_levelCounter);

            //OnChangeState?.Invoke(State.Start);
        }

        public void Cleanup()
        {
            _ballLandingController.Cleanup();
        }
    }
}

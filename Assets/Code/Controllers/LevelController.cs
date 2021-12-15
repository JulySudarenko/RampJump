using System;
using Code.Configs;
using Code.GameState;
using Code.Interfaces;
using Code.LevelConstructor;
using Code.Models;
using Code.View;
using UnityEngine;

namespace Code.Controllers
{
    internal class LevelController : IInitialize, ICleanup, IState
    {
        public event Action<State> OnChangeState;
        public LevelComponentsList CoinsList { get; }

        private readonly IBallModel _ball;
        private readonly BallLandingController _ballLandingController;
        private readonly EndGameView _endGameView;
        private readonly LevelObjectsConfigParser _configParser;
        private readonly Transform _hole;
        private readonly Transform _arrow;
        private int _levelCounter = 1;

        public LevelController(EndGameView endGameView,
            LevelObjectConfig[] config, IBallModel ball, Transform hole, Transform arrow)
        {
            _endGameView = endGameView;
            _ball = ball;
            _hole = hole;
            _arrow = arrow;
            _endGameView.NextLevelButton.onClick.AddListener(_endGameView.Restart);
            _endGameView.RestartLevelButton.onClick.AddListener(_endGameView.Restart);
            _configParser = new LevelObjectsConfigParser(config);
            _ballLandingController = new BallLandingController(_configParser.Bottom, ball);
            CoinsList = _configParser.CoinsList;
        }

        public void Initialize()
        {
            _endGameView.Restart();
            _configParser.InitNewLevel(_levelCounter);
            UpdateStartPositions();
            _ballLandingController.Init();
        }

        public void ChangeState(State state)
        {
            switch (state)
            {
                case State.Start:
                    UpdateStartPositions();
                    break;
                case State.BallTouched:
                    break;
                case State.BallKicked:
                    _configParser.BallStartPlace.gameObject.SetActive(false);
                    break;
                case State.Victory:
                    PlayVictoryVariant();
                    UpdateStartPositions();
                    break;
                case State.Defeat:
                    PlayDefeatVariant();
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

            _endGameView.ShowWinPanel();
            _configParser.InitNewLevel(_levelCounter);
            OnChangeState?.Invoke(State.Start);
        }

        private void PlayDefeatVariant()
        {
            _endGameView.ShowLosePanel();

            OnChangeState?.Invoke(State.Start);
        }

        private void UpdateStartPositions()
        {
            _configParser.BallStartPlace.gameObject.SetActive(true);
            _ball.Ball.position = _configParser.BallStartPosition;
            _ball.BallRigidbody.velocity = Vector3.zero;
            _ball.BallRigidbody.angularVelocity = Vector3.zero;
            _arrow.position = _configParser.BallStartPosition;
            _hole.position = _configParser.HoleStartPosition;

            _configParser.ReloadCoins(_levelCounter);
        }

        public void Cleanup()
        {
            _ballLandingController.Cleanup();
            _endGameView.NextLevelButton.onClick.RemoveAllListeners();
            _endGameView.RestartLevelButton.onClick.RemoveAllListeners();
        }
    }
}

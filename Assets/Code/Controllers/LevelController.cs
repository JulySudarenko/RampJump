using Code.Configs;
using Code.Interfaces;
using Code.Models;
using Code.UniversalFactory;
using Code.View;
using UnityEngine;

namespace Code.Controllers
{
    internal class LevelController : IInitialize, ICleanup
    {
        private readonly IFinishEvents _finishEvents;
        private readonly IBallEvents _ballEvents;
        private readonly EndGameView _endGameView;
        private readonly ILevel _configParser;
        private readonly IBallModel _ball;
        private readonly Transform _hole;
        private readonly Transform _arrow;
        private int _levelCounter = 1;
        
        public LevelController(IFinishEvents finishEvents, IBallEvents ballEvents, EndGameView endGameView, 
            LevelObjectConfig[] config, IBallModel ball, Transform hole, Transform arrow)
        {
            _finishEvents = finishEvents;
            _ballEvents = ballEvents;
            _endGameView = endGameView;
            _ball = ball;
            _hole = hole;
            _arrow = arrow;
            _endGameView.NextLevelButton.onClick.AddListener(_endGameView.Restart);
            _endGameView.RestartLevelButton.onClick.AddListener(_endGameView.Restart);
            _configParser = new LevelObjectsConfigParser(config);
        }
        
        public void Initialize()
        {
            _endGameView.Restart();
            _configParser.InitNewLevel(_levelCounter);
            UpdateStartPositions();
            _finishEvents.OnDefeat += PlayDefeatVariant;
            _finishEvents.OnVictory += PlayVictoryVariant;
            _ballEvents.OnBallKicked += OnBallKickedChange;
        }

        private void OnBallKickedChange(bool value)
        {
            _configParser.BallStartPlace.gameObject.SetActive(false);
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
            
            UpdateStartPositions();
        }
        
        private void PlayDefeatVariant()
        {
            _endGameView.ShowLosePanel();

            UpdateStartPositions();
        }

        private void UpdateStartPositions()
        {
            _configParser.BallStartPlace.gameObject.SetActive(true);
            _ball.Ball.position = _configParser.BallStartPosition;
            _ball.BallRigidbody.velocity = Vector3.zero;
            _ball.BallRigidbody.angularVelocity = Vector3.zero;
            _arrow.position = _configParser.BallStartPosition;
            _hole.position = _configParser.HoleStartPosition;
        }

        public void Cleanup()
        {
            _finishEvents.OnDefeat -= PlayDefeatVariant;
            _finishEvents.OnVictory -= PlayVictoryVariant;
            _ballEvents.OnBallKicked -= OnBallKickedChange;
        }
    }
}

using System;
using Code.Controllers;
using Code.Interfaces;

namespace Code.GameState
{
    internal class StateController : IInitialize, ICleanup
    {
        private readonly IState _ballController;
        private readonly IState _arrowController;
        private readonly IState _gameplayController;
        private readonly IState _levelController;
        private readonly IState _coinsController;
        private State _state;

        public StateController(BallTouchController ballEvents, ArrowController arrowController,
            GameplayController gameplayController, LevelController levelController, CoinsController coins)
        {
            _ballController = ballEvents;
            _gameplayController = gameplayController;
            _arrowController = arrowController;
            _levelController = levelController;
            _coinsController = coins;
        }

        private void ChangeState(State state)
        {
            _state = state;
            switch (_state)
            {
                case State.Start:
                    _levelController.ChangeState(_state);
                    _ballController.ChangeState(_state);
                    _coinsController.ChangeState(_state);
                    _gameplayController.ChangeState(_state);
                    break;
                case State.BallTouched:
                    _arrowController.ChangeState(_state);
                    _ballController.ChangeState(_state);
                    _coinsController.ChangeState(_state);
                    _gameplayController.ChangeState(_state);
                    break;
                case State.BallKicked:
                    _arrowController.ChangeState(_state);
                    _gameplayController.ChangeState(_state);
                    _levelController.ChangeState(_state);
                    _ballController.ChangeState(_state);
                    break;
                case State.Victory:
                    _gameplayController.ChangeState(_state);
                    _coinsController.ChangeState(_state);
                    _levelController.ChangeState(_state);
                    break;
                case State.Defeat:
                    _gameplayController.ChangeState(_state);
                    _coinsController.ChangeState(_state);
                    _levelController.ChangeState(_state);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Initialize()
        {
            _ballController.OnChangeState += ChangeState;
            _gameplayController.OnChangeState += ChangeState;
            _levelController.OnChangeState += ChangeState;
        }

        public void Cleanup()
        {
            _ballController.OnChangeState -= ChangeState;
            _gameplayController.OnChangeState -= ChangeState;
            _levelController.OnChangeState -= ChangeState;
        }
    }
}

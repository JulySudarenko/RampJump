using System;
using Code.Controllers;
using Code.Interfaces;

namespace Code.GameState
{
    internal class StateController : IInitialize, ICleanup
    {
        public Action<State> OnChangeState;

        private readonly IState _ballActions;
        private readonly IState _arrowController;
        private readonly IState _gameplayActions;
        private readonly IState _levelController;
        private State _state;

        public StateController(BallTouchController ballEvents, ArrowController arrowController,
            GameplayController gameplayController, LevelController levelController)
        {
            _ballActions = ballEvents;
            _gameplayActions = gameplayController;
            _arrowController = arrowController;
            _levelController = levelController;
        }

        private void ChangeState(State state)
        {
            _state = state;
            switch (_state)
            {
                case State.Start:
                    _levelController.ChangeState(_state);
                    _ballActions.ChangeState(_state);
                    break;
                case State.BallTouched:
                    _arrowController.ChangeState(_state);
                    _ballActions.ChangeState(_state);
                    break;
                case State.BallKicked:
                    _arrowController.ChangeState(_state);
                    _gameplayActions.ChangeState(_state);
                    _levelController.ChangeState(_state);
                    _ballActions.ChangeState(_state);
                    break;
                case State.Victory:
                    _gameplayActions.ChangeState(_state);
                    _levelController.ChangeState(_state);
                    break;
                case State.Defeat:
                    _gameplayActions.ChangeState(_state);
                    _levelController.ChangeState(_state);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Initialize()
        {
            _ballActions.OnChangeState += ChangeState;
            _gameplayActions.OnChangeState += ChangeState;
            _levelController.OnChangeState += ChangeState;
        }

        public void Cleanup()
        {
            _ballActions.OnChangeState -= ChangeState;
            _gameplayActions.OnChangeState -= ChangeState;
            _levelController.OnChangeState -= ChangeState;
        }
    }
}

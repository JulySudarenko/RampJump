using System.Collections.Generic;
using Code.Controllers;
using Code.Interfaces;

namespace Code.GameState
{
    internal class StateController : IInitialize, ICleanup
    {
        private readonly List<IState> _stateList;

        private readonly IState _ballController;
        private readonly IState _arrowController;
        private readonly IState _gameplayController;
        private readonly IState _levelController;
        private readonly IState _coinsController;
        private readonly IState _viewController;
        
        private State _state;

        public StateController(BallTouchController ballEvents, ArrowController arrowController,
            GameplayController gameplayController, LevelController levelController, CoinsController coins,
            ViewController viewController)
        {
            _ballController = ballEvents;
            _gameplayController = gameplayController;
            _arrowController = arrowController;
            _levelController = levelController;
            _coinsController = coins;
            _viewController = viewController;

            _stateList = new List<IState>();
            AddToStateList();
            _state = State.Start;
        }

        private void AddToStateList()
        {
            _stateList.Add(_viewController);
            _stateList.Add(_levelController);
            _stateList.Add(_ballController);
            _stateList.Add(_coinsController);
            _stateList.Add(_gameplayController);
            _stateList.Add(_arrowController);
        }

        private void ChangeState(State state)
        {
            _state = state;
            for (int i = 0; i < _stateList.Count; i++)
            {
                _stateList[i].ChangeState(_state);
            }
        }

        public void Initialize()
        {
            _ballController.OnChangeState += ChangeState;
            _gameplayController.OnChangeState += ChangeState;
            _levelController.OnChangeState += ChangeState;
            _viewController.OnChangeState += ChangeState;
        }

        public void Cleanup()
        {
            _ballController.OnChangeState -= ChangeState;
            _gameplayController.OnChangeState -= ChangeState;
            _levelController.OnChangeState -= ChangeState;
            _viewController.OnChangeState -= ChangeState;
        }
    }
}

using System;
using Code.GameState;
using Code.Interfaces;
using Code.Timer;
using Code.View;
using Code.ViewCommand;
using UnityEngine;

namespace Code.Controllers
{
    public class ViewController : IInitialize, ICleanup, IState
    {
        private const float LOADING_TIME = 2.0f;
        public event Action<State> OnChangeState;

        private ITimeRemaining _timeRemaining;
        private readonly PanelCommand _panelCommand;
        private readonly EndGameView _endGameView;
        private readonly MenuView _menuView;

        public ViewController(EndGameView endGameView, MenuView menuView, GameObject loadingPanelView)
        {
            _endGameView = endGameView;
            _menuView = menuView;
            _panelCommand = new PanelCommand(_endGameView, _menuView, loadingPanelView);
            _panelCommand.MakeStartUIPanel();
        }

        public void Initialize()
        {
            _endGameView.Restart();
            _endGameView.NextLevelButton.onClick.AddListener(_endGameView.Restart);
            _endGameView.NextLevelButton.onClick.AddListener(RestartLevel);
            _endGameView.RestartLevelButton.onClick.AddListener(_endGameView.Restart);
            _endGameView.RestartLevelButton.onClick.AddListener(RestartLevel);
            _menuView.Restart.onClick.AddListener(RestartLevel);
        }

        public void ChangeState(State state)
        {
            switch (state)
            {
                case State.Start:
                    _panelCommand.ChangePanel(StateUI.MenuView);
                    break;
                case State.BallTouched:
                    break;
                case State.BallKicked:
                    break;
                case State.Victory:
                    _panelCommand.ChangePanel(StateUI.EndGameView);
                    _endGameView.ShowWinPanel();
                    break;
                case State.Defeat:
                    _panelCommand.ChangePanel(StateUI.EndGameView);
                    _endGameView.ShowLosePanel();
                    break;
                case State.Loading:
                    _panelCommand.ChangePanel(StateUI.LoadingView);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void RestartLevel()
        {
            OnChangeState?.Invoke(State.Loading);
            _timeRemaining = new TimeRemaining(WaitBeforeStartGame, LOADING_TIME);
            _timeRemaining.AddTimeRemaining();
        }


        private void WaitBeforeStartGame()
        {
            OnChangeState?.Invoke(State.Start);
            _timeRemaining.RemoveTimeRemaining();
        }

        public void Cleanup()
        {
            _endGameView.NextLevelButton.onClick.RemoveAllListeners();
            _endGameView.RestartLevelButton.onClick.RemoveAllListeners();
            _menuView.Restart.onClick.RemoveAllListeners();
        }
    }
}

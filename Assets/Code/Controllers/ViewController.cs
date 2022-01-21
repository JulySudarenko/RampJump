using System;
using Code.Configs;
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
        private const float LOADING_TIME = 1.0f;
        public event Action<State> OnChangeState;

        private ITimeRemaining _timeRemaining;
        private readonly PanelCommand _panelCommand;
        private readonly EndGameView _endGameView;
        private readonly MenuView _menuView;
        private readonly GameMenu _gameMenu;

        public ViewController(ViewConfig viewConfig, Canvas canvas, EndGameView endGameView, MenuView menuView, GameObject loadingPanelView)
        {
            ViewInstaller viewInstaller = new ViewInstaller(viewConfig, canvas);
            _endGameView = endGameView;
            _menuView = menuView;
            _gameMenu = new GameMenu(_menuView);
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
            _menuView.Exit.onClick.AddListener(_gameMenu.Exit);
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
            _menuView.Exit.onClick.RemoveAllListeners();
        }
    }
}

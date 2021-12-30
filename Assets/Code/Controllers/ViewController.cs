using System;
using Code.GameState;
using Code.Interfaces;
using Code.View;
using Code.ViewCommand;

namespace Code.Controllers
{
    public class ViewController : IInitialize, ICleanup, IState
    {
        public event Action<State> OnChangeState;

        private readonly PanelCommand _panelCommand;
        private readonly EndGameView _endGameView;
        private readonly MenuView _menuView;

        public ViewController(EndGameView endGameView, MenuView menuView)
        {
            _endGameView = endGameView;
            _menuView = menuView;
            _panelCommand = new PanelCommand(_endGameView, _menuView);
            _panelCommand.MakeStartPosition();
        }

        public void Initialize()
        {
            _endGameView.Restart();
            _endGameView.NextLevelButton.onClick.AddListener(_endGameView.Restart);
            _endGameView.NextLevelButton.onClick.AddListener(_panelCommand.ChangePanelOnClick);
            _endGameView.RestartLevelButton.onClick.AddListener(_endGameView.Restart);
            _endGameView.RestartLevelButton.onClick.AddListener(_panelCommand.ChangePanelOnClick);
            _menuView.Restart.onClick.AddListener(RestartLevel);
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
                    _panelCommand.ChangePanel(StateUI.EndGameView);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void RestartLevel()
        {
            OnChangeState?.Invoke(State.Loading);
            _panelCommand.ChangePanel(StateUI.MenuView);
        }
        

        public void Cleanup()
        {
            _endGameView.NextLevelButton.onClick.RemoveAllListeners();
            _endGameView.RestartLevelButton.onClick.RemoveAllListeners();
            _menuView.Restart.onClick.RemoveAllListeners();
        }
    }
}

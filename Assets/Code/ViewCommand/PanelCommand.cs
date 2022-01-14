using System;
using Code.Controllers;
using Code.View;

namespace Code.ViewCommand
{
    internal class PanelCommand
    {
        private readonly EndGameViewCommand _endGameView;
        private readonly MenuViewCommand _menuView;
        private MainUICommandBase _currentPanelView;

        public PanelCommand(EndGameView endGameView, MenuView menuView)
        {
            _endGameView = new EndGameViewCommand(endGameView);
            _menuView = new MenuViewCommand(menuView);
        }
        
        public void MakeStartPosition()
        {
            _currentPanelView = _menuView;
        }

        public void ChangePanel(StateUI stateUI)
        {
            if (_currentPanelView != null)
            {
                _currentPanelView.Cancel();


                switch (stateUI)
                {
                    case StateUI.None:
                        break;
                    case StateUI.MenuView:
                        _currentPanelView = _menuView;
                        break;
                    case StateUI.EndGameView:
                        _currentPanelView = _endGameView;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(stateUI), stateUI, null);
                }

                _currentPanelView.Activate();
            }
        }
        
        public void ChangePanelOnClick()
        {
            ChangePanel(StateUI.MenuView);
        }
    }
}

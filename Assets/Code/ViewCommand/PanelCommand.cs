using System;
using System.Runtime.InteropServices;
using Code.Controllers;
using Code.Timer;
using Code.View;
using UnityEngine;


namespace Code.ViewCommand
{
    internal class PanelCommand
    {
        private readonly EndGameViewCommand _endGameView;
        private readonly MenuViewCommand _menuView;
        private readonly LoadingViewCommand _loadingViewCommand;
        private MainUICommandBase _currentPanelView;

        public PanelCommand(EndGameView endGameView, MenuView menuView, GameObject loadingPanelView)
        {
            _endGameView = new EndGameViewCommand(endGameView);
            _menuView = new MenuViewCommand(menuView);
            _loadingViewCommand = new LoadingViewCommand(loadingPanelView);
        }

        public void MakeStartUIPanel()
        {
            _currentPanelView = _menuView;
            _currentPanelView.Activate();
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
                    case StateUI.LoadingView:
                        _currentPanelView = _loadingViewCommand;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(stateUI), stateUI, null);
                }

                _currentPanelView.Activate();
            }
        }

        public void ChangePanelOnClick()
        {
            ChangePanel(StateUI.LoadingView);
        }
    }
}

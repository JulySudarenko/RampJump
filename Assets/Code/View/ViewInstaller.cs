using Code.Configs;
using Code.UniversalFactory;
using Code.View;
using UnityEngine;

namespace Code.Controllers
{
    internal class ViewInstaller
    {
        private MenuView _gameMenuView;
        private EndGameView _endGameView;
        private LoadingPanelView _loadingPanelView;
        private CoinCounterView _counterView;
        private StarEffectView _starEffectView;

        public MenuView GameMenuView => _gameMenuView;

        public EndGameView EndGameView => _endGameView;

        public LoadingPanelView LoadingPanelView => _loadingPanelView;

        public CoinCounterView CoinCounterView => _counterView;

        public StarEffectView StarEffectView => _starEffectView;

        public ViewInstaller(ViewConfig config, Canvas canvas)
        {
            CreateGameMenu(config, canvas);
            CreateEndGameMenu(config, canvas);
            CreateLoadingScreen(config, canvas);
        }

        private void CreateGameMenu(ViewConfig config, Canvas canvas)
        {
            var obj = new ObjectInitialization(new Factory(config.GameMenuView)).Create();
            obj.SetParent(canvas.transform);
            _gameMenuView = obj.GetComponent<MenuView>();
            _counterView = obj.GetComponentInChildren<CoinCounterView>();
        }

        private void CreateEndGameMenu(ViewConfig config, Canvas canvas)
        {
            var obj = new ObjectInitialization(new Factory(config.EndGameView.transform)).Create();
            obj.SetParent(canvas.transform);
            _endGameView = obj.GetComponent<EndGameView>();
            _starEffectView = obj.GetComponentInChildren<StarEffectView>(); 
        }

        private void CreateLoadingScreen(ViewConfig config, Canvas canvas)
        {
            var obj = new ObjectInitialization(new Factory(config.LoadingPanelView.transform)).Create();
            obj.SetParent(canvas.transform);
            _loadingPanelView = obj.GetComponent<LoadingPanelView>();
        }
    }
}

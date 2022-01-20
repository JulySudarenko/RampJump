using Code.Interfaces;
using UnityEngine;

namespace Code.Controllers
{
    public class GameMenu : IInitialize, ICleanup
    {
        private readonly MenuView _menuView;

        public GameMenu(MenuView menuView)
        {
            _menuView = menuView;
        }

        public void Initialize()
        {
            _menuView.Exit.onClick.AddListener(Exit);
        }

        private void Exit()
        {
            Application.Quit();
        }

        public void Cleanup()
        {
            _menuView.Exit.onClick.RemoveAllListeners();
        }
    }
}

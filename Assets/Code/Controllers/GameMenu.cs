using Code.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Controllers
{
    public class GameMenu : IInitialize, ICleanup
    {
        private readonly MenuView _menuView;
        private readonly Button _exit;

        public GameMenu(MenuView menuView)
        {
            _menuView = menuView;
        }

        public void Initialize()
        {
            _menuView.Exit.onClick.AddListener(Exit);
        }

        public void Exit()
        {
            Application.Quit();
        }

        public void Cleanup()
        {
            _menuView.Exit.onClick.RemoveAllListeners();
        }
    }
}

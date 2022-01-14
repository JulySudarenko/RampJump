using Code.Controllers;

namespace Code.ViewCommand
{
    public sealed class MenuViewCommand : MainUICommandBase
    {
        private readonly MenuView _menuPanel;

        public MenuViewCommand(MenuView panel)
        {
            _menuPanel = panel;
        }
        
        public override void Activate()
        {
            _menuPanel.MenuPanel.SetActive(true);
            _menuPanel.gameObject.SetActive(true);
        }

        public override void Cancel()
        {
            _menuPanel.MenuPanel.SetActive(false);
            _menuPanel.gameObject.SetActive(false);
        }
    }
}

using UnityEngine;

namespace Code.ViewCommand
{
    public sealed class LoadingViewCommand : MainUICommandBase
    {
        private readonly GameObject _loadingGamePanel;

        public LoadingViewCommand(GameObject loadingPanel)
        {
            _loadingGamePanel = loadingPanel;
        }
        
        public override void Activate()
        {
            _loadingGamePanel.SetActive(true);
        }

        public override void Cancel()
        {
            _loadingGamePanel.SetActive(false);
        }
    }
}

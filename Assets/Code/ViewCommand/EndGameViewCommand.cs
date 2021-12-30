using Code.View;

namespace Code.ViewCommand
{
    public sealed class EndGameViewCommand : MainUICommandBase
    {
        private readonly EndGameView _endGamePanel;

        public EndGameViewCommand(EndGameView panel)
        {
            _endGamePanel = panel;
        }
        
        public override void Activate()
        {
            _endGamePanel.gameObject.SetActive(true);
        }

        public override void Cancel()
        {
            _endGamePanel.gameObject.SetActive(false);
        }
    }
}

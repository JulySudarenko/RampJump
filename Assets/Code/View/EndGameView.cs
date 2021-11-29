using UnityEngine;
using UnityEngine.UI;

namespace Code.View
{
    internal sealed class EndGameView : MonoBehaviour
    {
        [SerializeField] private GameObject _gamePanel;
        public Button NextLevelButton;
        public Button RestartLevelButton;


        public void Restart()
        {
            _gamePanel.SetActive(false);
            NextLevelButton.gameObject.SetActive(false);
            RestartLevelButton.gameObject.SetActive(false);
        }

        public void ShowWinPanel()
        {
            _gamePanel.SetActive(true);
            NextLevelButton.gameObject.SetActive(true);
        }

        public void ShowLosePanel()
        {
            _gamePanel.SetActive(true);
            RestartLevelButton.gameObject.SetActive(true);
        }
    }
}

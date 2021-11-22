using UnityEngine;
using UnityEngine.UI;

namespace Code.View
{
    internal sealed class EndGameView : MonoBehaviour
    {
        [SerializeField] private GameObject _gamePanel;
        [SerializeField] private Image _happySmile;
        [SerializeField] private Image _sadSmile;
        public Button NextLevelButton;
        public Button RestartLevelButton;


        public void Restart()
        {
            _gamePanel.SetActive(false);
            _happySmile.gameObject.SetActive(false);
            _sadSmile.gameObject.SetActive(false);
            NextLevelButton.gameObject.SetActive(false);
            RestartLevelButton.gameObject.SetActive(false);
        }

        public void ShowWinPanel()
        {
            _gamePanel.SetActive(true);
            _happySmile.gameObject.SetActive(true);
            NextLevelButton.gameObject.SetActive(true);
        }

        public void ShowLosePanel()
        {
            _gamePanel.SetActive(true);
            _sadSmile.gameObject.SetActive(true);
            RestartLevelButton.gameObject.SetActive(true);
        }
    }
}

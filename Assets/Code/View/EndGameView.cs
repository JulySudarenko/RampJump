using UnityEngine;
using UnityEngine.UI;

namespace Code.View
{
    public sealed class EndGameView : MonoBehaviour
    {
        [SerializeField] private GameObject _gamePanel;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _restartLevelButton;
        [SerializeField] private Image _happySmile;
        [SerializeField] private Image _sadSmile;
        [SerializeField] private StarEffectView _stars;

        public GameObject EndGamePanel => _gamePanel;
        public Button NextLevelButton => _nextLevelButton;
        public Button RestartLevelButton => _restartLevelButton;

        public void Restart()
        {
            _gamePanel.SetActive(false);
            _nextLevelButton.gameObject.SetActive(false);
            _restartLevelButton.gameObject.SetActive(false);
            _happySmile.gameObject.SetActive(false);
            _sadSmile.gameObject.SetActive(false);
            _stars.gameObject.SetActive(false);
        }

        public void ShowWinPanel()
        {
            _gamePanel.SetActive(true);
            _nextLevelButton.gameObject.SetActive(true);
            _happySmile.gameObject.SetActive(true);
            _stars.gameObject.SetActive(true);
        }

        public void ShowLosePanel()
        {
            _gamePanel.SetActive(true);
            _restartLevelButton.gameObject.SetActive(true);
            _sadSmile.gameObject.SetActive(true);
        }
    }
}

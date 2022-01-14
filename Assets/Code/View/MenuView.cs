using UnityEngine;
using UnityEngine.UI;

namespace Code.Controllers
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _restart;
        [SerializeField] private Button _exit;

        public Button Restart => _restart;
        public Button Exit => _exit;
        public GameObject MenuPanel => _panel;
    }
}

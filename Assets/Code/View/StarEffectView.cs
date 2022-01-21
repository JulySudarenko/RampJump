using UnityEngine;
using UnityEngine.UI;

namespace Code.View
{
    public class StarEffectView : MonoBehaviour
    {
        [SerializeField] private Image _largeFillStar;
        [SerializeField] private Image _leftFillStar;
        [SerializeField] private Image _rightFillStar;

        public void ShowStars(bool large, bool left, bool right)
        {
            _largeFillStar.gameObject.SetActive(large);
            _leftFillStar.gameObject.SetActive(left);
            _rightFillStar.gameObject.SetActive(right);
        }


    }
}

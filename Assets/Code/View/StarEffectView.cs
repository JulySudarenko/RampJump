using UnityEngine;
using UnityEngine.UI;

namespace Code.View
{
    internal class StarEffectView : MonoBehaviour
    {
        [SerializeField] private Image _largeFillStar;
        [SerializeField] private Image _leftFillStar;
        [SerializeField] private Image _rightFillStar;

        private void ShowStars(bool large, bool left, bool right)
        {
            _largeFillStar.gameObject.SetActive(large);
            _leftFillStar.gameObject.SetActive(left);
            _rightFillStar.gameObject.SetActive(right);
        }

        public void ShowEndStarEffect(int winStarsQuantity)
        {
            switch (winStarsQuantity)
            {
                case 1:
                    ShowStars(true, false, false);
                    break;
                case 2:
                    ShowStars(true, true, false);
                    break;
                case 3:
                    ShowStars(true, true, true);
                    break;
                default:
                    ShowStars(false,false,false);
                    break;
            }
        }
    }
}

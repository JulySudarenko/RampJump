using TMPro;
using UnityEngine;

namespace Code.View
{
    internal class CoinCounterView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coinCount;
        [SerializeField] private GameObject _panel;

        public void ShowCoinsCount(int coins, int coinsMax)
        {
            _coinCount.text = $"{coins} / {coinsMax}";
        }
    }
}

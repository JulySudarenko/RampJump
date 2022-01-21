using Code.View;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "ViewConfig", menuName = "Configs/ViewConfig", order = 0)]
    public class ViewConfig : ScriptableObject
    {
        public Transform GameMenuView;
        public CoinCounterView CounterView;
        public EndGameView EndGameView;
        public StarEffectView StarEffectView;
        public LoadingPanelView LoadingPanelView;
    }
}

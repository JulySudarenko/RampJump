using UnityEngine;

namespace RampJump
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private GameObject _ball;
        [SerializeField] private GameObject _glass;
        [SerializeField] private GameObject _arrow;
        [SerializeField] private GameObject _level;

        [SerializeField] private Transform _ballStartPosition;

        private BallTouchHandling _ballTouchHandling;
        private GlassTouchHandling _glassTouchHandling;

        void Start()
        {
            _ballTouchHandling = new BallTouchHandling(_ball, _ballStartPosition);
            _glassTouchHandling = new GlassTouchHandling(_glass);
        }

        void Update()
        {
            _glassTouchHandling.Execute(Time.deltaTime);
            _ballTouchHandling.Execute(Time.deltaTime);
        }
    }
}

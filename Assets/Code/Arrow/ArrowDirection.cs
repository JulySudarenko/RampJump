using Code.Ball;
using UnityEngine;

namespace Code.Controllers
{
    internal class ArrowDirection
    {
        private readonly Transform _arrow;
        private readonly Transform _ball;
        private Vector3 _mousePosition;
        private Vector3 _newPosition;
        private Vector3 _newDirection;
        private Vector3 _touchStartPosition;
        private float _deltaPositionX;
        private float _deltaPositionY;

        public ArrowDirection(Transform arrow, Transform ball)
        {
            _arrow = arrow;
            _ball = ball;
            _arrow.gameObject.SetActive(false);
        }

        public void Run(Vector3 mousePosition)
        {
            _arrow.gameObject.SetActive(true);
            _arrow.position = _ball.position;
            _touchStartPosition = new Vector3(mousePosition.x, mousePosition.y, _arrow.position.z);
        }

        public void FollowDirection(Vector3 mousePosition)
        {
            _newDirection =
                new Vector3(mousePosition.x, mousePosition.y, _arrow.position.z) - _touchStartPosition;
            _arrow.eulerAngles =
                new Vector3(0, 0, Mathf.Atan2(_newDirection.y, _newDirection.x) * Mathf.Rad2Deg - 180);
        }

        public void TurnOff()
        {
            _arrow.gameObject.SetActive(false);
        }
    }
}

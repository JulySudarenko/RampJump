using Code.Interfaces;
using Code.UserInput;
using UnityEngine;

namespace Code.Controllers
{
    internal sealed class ArrowController : IInitialize, IFixedExecute, ICleanup
    {
        private readonly IUserInput _userInput;
        private readonly IBallEvents _ballEvents;
        private readonly Transform _arrow;
        private Vector3 _mousePosition;
        private Vector3 _newPosition;
        private Vector3 _newDirection;
        private Vector3 _touchStartPosition;
        private float _deltaPositionX;
        private float _deltaPositionY;
        private bool _isMouseButton;
        private bool _isBallTouched;
        private bool _isMouseButtonDown;

        public ArrowController(IBallEvents ballEvents, Transform arrow,
            IUserInput input)
        {
            _arrow = arrow;
            _ballEvents = ballEvents;
            _userInput = input;
        }

        public void Initialize()
        {
            _ballEvents.OnBallTouched += IsBallTouched;
            _userInput.OnTouch += OnMouseButton;
            _userInput.OnTouchDown += OnMouseButtonDown;
            _userInput.OnChangeMousePosition += GetMousePosition;
            _arrow.gameObject.SetActive(false);
        }

        private void IsBallTouched(bool value)
        {
            _isBallTouched = value;
            _arrow.gameObject.SetActive(value);
        }

        private void OnMouseButtonDown(bool value) => _isMouseButtonDown = value;
        private void GetMousePosition(Vector3 position) => _mousePosition = position;
        private void OnMouseButton(bool value) => _isMouseButton = value;

        public void FixedExecute(float deltaTime)
        {
            if (_isBallTouched && _isMouseButtonDown)
            {
                _touchStartPosition = new Vector3(_mousePosition.x, _mousePosition.y,
                    _arrow.position.z);
            }

            if (_isBallTouched && _isMouseButton)
            {
                FollowDirection();
            }
        }

        private void FollowDirection()
        {
            _newDirection =
                new Vector3(_mousePosition.x, _mousePosition.y, _arrow.position.z) - _touchStartPosition;
            _arrow.eulerAngles =
                new Vector3(0, 0, Mathf.Atan2(_newDirection.y, _newDirection.x) * Mathf.Rad2Deg - 180);

            _arrow.localScale = new Vector3(3.0f, 1.0f, 1.0f);
        }

        public void Cleanup()
        {
            _ballEvents.OnBallTouched -= IsBallTouched;
            _userInput.OnChangeMousePosition -= GetMousePosition;
            _userInput.OnTouchDown -= OnMouseButtonDown;
            _userInput.OnTouch -= OnMouseButton;
        }
    }
}

using Code.Interfaces;
using Code.UserInput;
using UnityEngine;

namespace Code.Controllers
{
    public class BallTouchHandlingController : IInitialize, IFixedExecute, ICleanup
    {
        private readonly GameObject _ball;
        private readonly GameObject _startPlace;
        private readonly IUserInput _userInput;
        private readonly Vector3 _startPosition;
        private readonly Camera _camera;
        private readonly float _force;
        private readonly Rigidbody _ballRigidbody;
        private readonly int _ballID;
        private Ray _ray;
        private RaycastHit _hit;
        private Vector3 _touchStartPosition;
        private Vector3 _touchDirection;
        private Vector3 _mousePosition;
        private bool _isBallTouched;
        private bool _isMouseButtonDown;
        private bool _isMouseButtonUp;

        public BallTouchHandlingController(GameObject ball, float speed, Vector3 startPosition, GameObject startPlace,
            Camera camera, IUserInput userInput)
        {
            _ball = ball;
            _startPlace = startPlace;
            _startPosition = startPosition;
            _force = speed;
            _camera = camera;
             _userInput = userInput;
             _ballRigidbody = _ball.GetComponentInChildren<Rigidbody>();
             _ballID = _ball.GetComponentInChildren<SphereCollider>().gameObject.GetInstanceID();
        }

        public void Initialize()
        {
            _ball.transform.position = _startPosition;
            _isBallTouched = false;
            _userInput.OnTouchDown += OnMouseButtonDown;
            _userInput.OnTouchUp += OnMouseButtonUp;
            _userInput.OnChangeMousePosition += GetMousePosition;
        }
        
        private void OnMouseButtonDown(bool value) => _isMouseButtonDown = value;
        private void OnMouseButtonUp(bool value) => _isMouseButtonUp = value;
        private void GetMousePosition(Vector3 position) => _mousePosition = position;
        
        public void FixedExecute(float deltaTime)
        {
            if (_isMouseButtonDown)
            {
                CheckTouch();
            }

            if (_isMouseButtonUp)
            {
                KickTheBall();
            }
        }

        private void CheckTouch()
        {
            _ray = _camera.ScreenPointToRay(_mousePosition);

            if (Physics.Raycast(_ray, out _hit, 30))
            {
                if (_hit.collider.gameObject.GetInstanceID() == _ballID)
                {
                    _isBallTouched = true;
                    _touchStartPosition = new Vector3(_mousePosition.x, _mousePosition.y,
                        _ball.transform.position.z);
                }
            }
        }

        private void KickTheBall()
        {
            if (_isBallTouched)
            {
                _startPlace.SetActive(false);
                _touchDirection =
                    new Vector3(_mousePosition.x, _mousePosition.y, _ball.transform.position.z);
                _ballRigidbody.AddForce((_touchDirection - _touchStartPosition).normalized * _force);
                _isBallTouched = false;
            }
        }

        public void Cleanup()
        {
            _userInput.OnTouchDown -= OnMouseButtonDown;
            _userInput.OnTouchUp -= OnMouseButtonUp;
            _userInput.OnChangeMousePosition -= GetMousePosition;
        }

    }
}  

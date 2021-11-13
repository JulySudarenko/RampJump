using Code.Interfaces;
using Code.UserInput;
using UnityEngine;

namespace Code.Controllers
{
    public class GlassTouchHandlingController : IInitialize, IFixedExecute, ICleanup
    {
        private readonly GameObject _glass;
        private readonly Vector3 _startPosition;
        private readonly Camera _camera;
        private readonly IUserInput _userInput;
        private readonly int _glassID;
        private float _speed;
        //private readonly float _speed = 3.0f;
        private Ray _ray;
        private RaycastHit _hit;

        private Vector3 _newPosition;
        private Vector3 _mousePosition;
        private float _deltaPositionX;
        private bool _isMouseButtonDown;
        private bool _isMouseButtonUp;
        private bool _isMouseButton;


        private bool _isGlassTouched;

        public GlassTouchHandlingController(GameObject glass, float speed, Vector3 startPosition, Camera camera,
            IUserInput userInput)
        {
            _glass = glass;
            _startPosition = startPosition;
            _camera = camera;
            _speed = speed;
            _userInput = userInput;
            _glassID = _glass.GetComponentInChildren<CapsuleCollider>().gameObject.GetInstanceID();
        }

        public void Initialize()
        {
            _glass.transform.position = _startPosition;
            _isGlassTouched = false;
            _userInput.OnTouchDown += OnMouseButtonDown;
            _userInput.OnTouchUp += OnMouseButtonUp;
            _userInput.OnTouch += OnMouseButton;
            _userInput.OnChangeMousePosition += GetMousePosition;
        }

        private void OnMouseButtonDown(bool value) => _isMouseButtonDown = value;
        private void OnMouseButtonUp(bool value) => _isMouseButtonUp = value;
        private void OnMouseButton(bool value) => _isMouseButton = value;

        private void GetMousePosition(Vector3 position) => _mousePosition = position;

        public void FixedExecute(float deltaTime)
        {
            if (_isMouseButtonDown)
            {
                CheckTouch();
            }

            if (_isMouseButton)
            {
                MoveTheGlass(deltaTime);
            }

            if (_isMouseButtonUp)
            {
                FinishMoving();
            }
        }

        private void CheckTouch()
        {
            _ray = _camera.ScreenPointToRay(_mousePosition);

            if (Physics.Raycast(_ray, out _hit, 30))
            {
                if (_hit.collider.gameObject.GetInstanceID() == _glassID)
                {
                    _isGlassTouched = true;
                }
            }
        }

        private void MoveTheGlass(float deltaTime)
        {
            if (_isGlassTouched)
            {
                _deltaPositionX = _camera.ScreenToWorldPoint(
                    new Vector3(_mousePosition.x, _mousePosition.y, _camera.transform.position.z)).x;
                _newPosition = new Vector3(_glass.transform.position.x - _deltaPositionX, _glass.transform.position.y,
                    _glass.transform.position.z);
                _glass.transform.position =
                    Vector3.Lerp(_glass.transform.position, _newPosition, deltaTime * _speed);
            }
        }

        private void FinishMoving()
        {
            _isGlassTouched = false;
        }

        public void Cleanup()
        {
            _userInput.OnTouchDown -= OnMouseButtonDown;
            _userInput.OnTouchUp -= OnMouseButtonUp;
            _userInput.OnTouch -= OnMouseButton;
            _userInput.OnChangeMousePosition -= GetMousePosition;
        }
    }
}

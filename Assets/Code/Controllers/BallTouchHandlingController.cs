using System;
using Code.Interfaces;
using Code.Models;
using Code.UserInput;
using UnityEngine;

namespace Code.Controllers
{
    public class BallTouchHandlingController : IInitialize, IFixedExecute, ICleanup, IBallEvents
    {
        public event Action<bool> OnBallTouched;
        public event Action<bool> OnBallKicked;

        private readonly IBallModel _ballModel;
        private readonly IUserInput _userInput;
        private readonly Camera _camera;
        private Ray _ray;
        private RaycastHit _hit;
        private Renderer _renderer;
        private Color _colorStart;
        private Vector3 _touchStartPosition;
        private Vector3 _touchDirection;
        private Vector3 _mousePosition;
        private float _force;
        private float _ballChangeColorSpeed;
        private float _forceRiseFactor = 50.0f;
        private float _colorRiseFactor = 5.0f;
        private bool _isBallTouched;
        private bool _isMouseButtonDown;
        private bool _isMouseButtonUp;
        private bool _isMouseButton;

        public BallTouchHandlingController(IBallModel ballModel, Camera camera, IUserInput userInput)
        {
            _ballModel = ballModel;
            _camera = camera;
            _userInput = userInput;
        }

        public void Initialize()
        {
            _isBallTouched = false;
            _userInput.OnTouchDown += OnMouseButtonDown;
            _userInput.OnTouchUp += OnMouseButtonUp;
            _userInput.OnTouch += OnMouseButton;
            _userInput.OnChangeMousePosition += GetMousePosition;
            _renderer = _ballModel.Ball.GetComponentInChildren<Renderer>();
            _colorStart = _renderer.material.color;
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

            if (_isBallTouched)
            {
                if (_isMouseButton)
                {
                    IncreaseTheSpeed(deltaTime);
                }

                if (_isMouseButtonUp)
                {
                    KickTheBall();
                }
            }
        }

        private void CheckTouch()
        {
            _ray = _camera.ScreenPointToRay(_mousePosition);

            if (Physics.Raycast(_ray, out _hit, 100))
            {
                if (_hit.collider.gameObject.GetInstanceID() == _ballModel.BallID)
                {
                    _isBallTouched = true;
                    OnBallTouched?.Invoke(true);
                    _touchStartPosition = new Vector3(_mousePosition.x, _mousePosition.y,
                        _ballModel.Ball.position.z);
                    _ballChangeColorSpeed = 0.0f;
                    _force = _ballModel.BallSpeed;
                }
            }
        }

        private void IncreaseTheSpeed(float deltaTime)
        {
            _force += deltaTime * _forceRiseFactor;
            _ballChangeColorSpeed += deltaTime / _colorRiseFactor;
            _renderer.material.color = Color.Lerp(Color.yellow, Color.red, _ballChangeColorSpeed);
        }


        private void KickTheBall()
        {
            _touchDirection =
                new Vector3(_mousePosition.x, _mousePosition.y, _ballModel.Ball.position.z);
            _ballModel.BallRigidbody.AddForce((_touchDirection - _touchStartPosition).normalized * _force);
            _isBallTouched = false;
            OnBallTouched?.Invoke(false);
            OnBallKicked?.Invoke(true);
            _renderer.material.color = _colorStart;
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

using System;
using Code.Ball;
using Code.GameState;
using Code.Interfaces;
using Code.UserInput;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Controllers
{
    public class BallTouchController : IInitialize, IExecute, ICleanup, IState
    {
        public event Action<State> OnChangeState;

        private const float HIT_DISTANCE = 100.0f;
        private const float MAX_FORCE = 3200.0f;
        private readonly IBall _ball;
        private readonly IForceModel _forceModel;
        private readonly IUserInput _userInput;
        private readonly Camera _camera;
        private State _state;
        private Ray _ray;
        private RaycastHit _hit;
        private Vector3 _touchStartPosition;
        private Vector3 _touchDirection;
        private Vector3 _mousePosition;
        private float _force;
        private float _ballChangeColorSpeed;
        private bool _isMouseButtonDown;
        private bool _isMouseButtonUp;
        private bool _isMouseButton;

        public BallTouchController(IBall ball, IForceModel forceModel, Camera camera,
            IUserInput userInput, [CanBeNull] AudioSource source, AudioClip clip)
        {
            _ball = ball;
            _camera = camera;
            _userInput = userInput;
            _forceModel = forceModel;
        }

        public void Initialize()
        {
            _userInput.OnTouchDown += OnMouseButtonDown;
            _userInput.OnTouchUp += OnMouseButtonUp;
            _userInput.OnTouch += OnMouseButton;
            _userInput.OnChangeMousePosition += GetMousePosition;
        }

        private void OnMouseButtonDown(bool value) => _isMouseButtonDown = value;
        private void OnMouseButtonUp(bool value) => _isMouseButtonUp = value;
        private void OnMouseButton(bool value) => _isMouseButton = value;
        private void GetMousePosition(Vector3 position) => _mousePosition = position;

        public void ChangeState(State state) => _state = state;

        public void Execute(float deltaTime)
        {
            if (_isMouseButtonDown && _state == State.Start)
            {
                CheckTouch();
            }

            if (_state == State.BallTouched)
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

            if (Physics.Raycast(_ray, out _hit, HIT_DISTANCE))
            {
                if (_hit.collider.gameObject.GetInstanceID() == _ball.BallID)
                {
                    OnChangeState?.Invoke(State.BallTouched);
                    _touchStartPosition = new Vector3(_mousePosition.x, _mousePosition.y,
                        _ball.BallTransform.position.z);
                    _force = _forceModel.BallForce;
                }
            }
        }

        private void IncreaseTheSpeed(float deltaTime)
        {
            _force += deltaTime * _forceModel.ForceRiseFactor;

            if (_force >= MAX_FORCE)
                _force = MAX_FORCE;
        }

        private void KickTheBall()
        {
            _touchDirection =
                new Vector3(_mousePosition.x, _mousePosition.y, _ball.BallTransform.position.z);
            _ball.BallRigidbody.AddForce((_touchDirection - _touchStartPosition).normalized * _force);
            OnChangeState?.Invoke(State.BallKicked);
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

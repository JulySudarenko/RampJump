using System;
using Code.GameState;
using Code.Interfaces;
using Code.Models;
using Code.UserInput;
using UnityEngine;

namespace Code.Controllers
{
    internal sealed class ArrowController : IInitialize, IExecute, ICleanup, IState
    {
        public event Action<State> OnChangeState;
        private readonly IUserInput _userInput;
        private readonly Transform _arrow;
        private readonly Transform _ball;
        private Renderer[] _renderers;
        private Color[] _colorStart;
        private State _state;
        private Vector3 _mousePosition;
        private Vector3 _newPosition;
        private Vector3 _newDirection;
        private Vector3 _touchStartPosition;
        private float _deltaPositionX;
        private float _deltaPositionY;
        private float _colorRiseFactor;
        private bool _isMouseButton;
        private bool _isMouseButtonDown;
        private float _arrowChangeColorSpeed;

        public ArrowController(Transform arrow, IUserInput input, Transform ball, IForceModel speedModel)
        {
            _arrow = arrow;
            _ball = ball;
            _userInput = input;
            _colorRiseFactor = speedModel.ColorRiseFactor;
        }

        public void Initialize()
        {
            _userInput.OnTouch += OnMouseButton;
            _userInput.OnTouchDown += OnMouseButtonDown;
            _userInput.OnChangeMousePosition += GetMousePosition;
            _arrow.gameObject.SetActive(false);

            _renderers = _arrow.GetComponentsInChildren<Renderer>();
            _colorStart = new Color[_renderers.Length];
            for (int i = 0; i < _renderers.Length; i++)
            {
                _colorStart[i] = _renderers[i].material.color;
            }
        }

        public void ChangeState(State state)
        {
            _state = state;
            switch (state)
            {
                case State.Start:
                    break;
                case State.BallTouched:
                    _arrow.gameObject.SetActive(true);
                    _arrow.position = _ball.position;
                    break;
                case State.BallKicked:
                    for (int i = 0; i < _renderers.Length; i++)
                    {
                        _renderers[i].material.color = _colorStart[i];
                    }
                    _arrow.gameObject.SetActive(false);
                    break;
                case State.Victory:
                    break;
                case State.Defeat:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void OnMouseButtonDown(bool value) => _isMouseButtonDown = value;
        private void GetMousePosition(Vector3 position) => _mousePosition = position;
        private void OnMouseButton(bool value) => _isMouseButton = value;

        public void Execute(float deltaTime)
        {
            if (_state == State.BallTouched && _isMouseButtonDown)
            {
                _touchStartPosition = new Vector3(_mousePosition.x, _mousePosition.y, _arrow.position.z);
                _arrowChangeColorSpeed = 0.0f;
            }

            if (_state == State.BallTouched && _isMouseButton)
            {
                FollowDirection(deltaTime);
            }
        }

        private void FollowDirection(float deltaTime)
        {
            _newDirection =
                new Vector3(_mousePosition.x, _mousePosition.y, _arrow.position.z) - _touchStartPosition;
            _arrow.eulerAngles =
                new Vector3(0, 0, Mathf.Atan2(_newDirection.y, _newDirection.x) * Mathf.Rad2Deg - 180);
            _arrowChangeColorSpeed += deltaTime / _colorRiseFactor;

            for (int i = 0; i < _renderers.Length; i++)
            {
                _renderers[i].material.color = Color.Lerp(Color.green, Color.red, _arrowChangeColorSpeed);
            }
        }

        public void Cleanup()
        {
            _userInput.OnChangeMousePosition -= GetMousePosition;
            _userInput.OnTouchDown -= OnMouseButtonDown;
            _userInput.OnTouch -= OnMouseButton;
        }
    }
}

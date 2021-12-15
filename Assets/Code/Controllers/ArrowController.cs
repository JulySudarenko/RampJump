using System;
using Code.GameState;
using Code.Interfaces;
using Code.LevelConstructor;
using Code.Models;
using Code.UniversalFactory;
using Code.UserInput;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Controllers
{
    internal sealed class ArrowController : IInitialize, IExecute, ICleanup, IState
    {
        public event Action<State> OnChangeState;
        private readonly IUserInput _userInput;
        private readonly StateController _state;
        private readonly Transform _arrow;
        private readonly Transform _ball;
        private Vector3 _mousePosition;
        private Vector3 _newPosition;
        private Vector3 _newDirection;
        private Vector3 _touchStartPosition;
        private float _deltaPositionX;
        private float _deltaPositionY;
        private bool _isMouseButton;
        private bool _isBallTouched;
        private bool _isMouseButtonDown;

        public ArrowController(Transform arrow, IUserInput input, Transform ball)
        {
            _arrow = arrow;
            _ball = ball;
            _userInput = input;
        }

        public void Initialize()
        {
            _userInput.OnTouch += OnMouseButton;
            _userInput.OnTouchDown += OnMouseButtonDown;
            _userInput.OnChangeMousePosition += GetMousePosition;
            _arrow.gameObject.SetActive(false);
        }

        public void ChangeState(State state)
        {
            if (state == State.BallTouched)
            {
                _isBallTouched = true;
                _arrow.gameObject.SetActive(true);
                _arrow.position = _ball.position;
                OnChangeState?.Invoke(state);
            }
            else
            {
                _isBallTouched = false;
                _arrow.gameObject.SetActive(false);
            }
        }

        private void OnMouseButtonDown(bool value) => _isMouseButtonDown = value;
        private void GetMousePosition(Vector3 position) => _mousePosition = position;
        private void OnMouseButton(bool value) => _isMouseButton = value;

        public void Execute(float deltaTime)
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
        }

        public void Cleanup()
        {
            _userInput.OnChangeMousePosition -= GetMousePosition;
            _userInput.OnTouchDown -= OnMouseButtonDown;
            _userInput.OnTouch -= OnMouseButton;
        }
    }

    internal class BallSoundController : IState
    {
        public event Action<State> OnChangeState;

        private LevelComponentsList _componentsList;
        private State _state;
        private AudioListener _listener;
        private AudioSource _source;
        private Hit _hitBall;

        public BallSoundController(IBallModel ballModel)
        {
        }

        public void ChangeState(State state) => _state = state;

        private void OnBallSlime()
        {
        }

        private void PlaySlimeSound()
        {
            //_source.clip = ;
            _source.Play();
        }

        private void StopSound()
        {
            _source.Stop();
        }
    }
}

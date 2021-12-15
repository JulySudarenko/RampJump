﻿using System;
using Code.GameState;
using Code.Interfaces;
using Code.Models;
using Code.UserInput;
using UnityEngine;

namespace Code.Controllers
{
    public class BallTouchController : IInitialize, IExecute, ICleanup, IState
    {
        public event Action<State> OnChangeState;

        private const float HIT_DISTANCE = 100.0f;
        private const float MAX_FORCE = 3200.0f;
        private readonly IBallModel _ballModel;
        private readonly IBallForceModel _ballForceModel;
        private readonly IUserInput _userInput;
        private readonly Camera _camera;
        private Ray _ray;
        private RaycastHit _hit;
        private Color _colorStart;
        private Vector3 _touchStartPosition;
        private Vector3 _touchDirection;
        private Vector3 _mousePosition;
        private float _force;
        private float _ballChangeColorSpeed;
        private bool _isBallTouched;
        private bool _isMouseButtonDown;
        private bool _isMouseButtonUp;
        private bool _isMouseButton;

        public BallTouchController(IBallModel ballModel, IBallForceModel forceModel, Camera camera,
            IUserInput userInput)
        {
            _ballModel = ballModel;
            _camera = camera;
            _userInput = userInput;
            _ballForceModel = forceModel;
        }

        public void Initialize()
        {
            _isBallTouched = false;
            _userInput.OnTouchDown += OnMouseButtonDown;
            _userInput.OnTouchUp += OnMouseButtonUp;
            _userInput.OnTouch += OnMouseButton;
            _userInput.OnChangeMousePosition += GetMousePosition;
            _colorStart = _ballModel.BallRenderer.material.color;
        }

        private void OnMouseButtonDown(bool value) => _isMouseButtonDown = value;
        private void OnMouseButtonUp(bool value) => _isMouseButtonUp = value;
        private void OnMouseButton(bool value) => _isMouseButton = value;
        private void GetMousePosition(Vector3 position) => _mousePosition = position;

        public void Execute(float deltaTime)
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

            Debug.Log(_ballModel.Ball.position);
        }

        private void CheckTouch()
        {
            _ray = _camera.ScreenPointToRay(_mousePosition);

            if (Physics.Raycast(_ray, out _hit, HIT_DISTANCE))
            {
                if (_hit.collider.gameObject.GetInstanceID() == _ballModel.BallID)
                {
                    _isBallTouched = true;
                    OnChangeState?.Invoke(State.BallTouched);
                    _touchStartPosition = new Vector3(_mousePosition.x, _mousePosition.y,
                        _ballModel.Ball.position.z);
                    _ballChangeColorSpeed = 0.0f;
                    _force = _ballForceModel.BallForce;
                }
            }
        }

        private void IncreaseTheSpeed(float deltaTime)
        {
            _force += deltaTime * _ballForceModel.ForceRiseFactor;
            _ballChangeColorSpeed += deltaTime / _ballForceModel.ColorRiseFactor;
            if (_force >= MAX_FORCE)
                _force = MAX_FORCE;
            _ballModel.BallRenderer.material.color = Color.Lerp(Color.yellow, Color.red, _ballChangeColorSpeed);
        }

        private void KickTheBall()
        {
            _touchDirection =
                new Vector3(_mousePosition.x, _mousePosition.y, _ballModel.Ball.position.z);
            _ballModel.BallRigidbody.AddForce((_touchDirection - _touchStartPosition).normalized * _force);
            _isBallTouched = false;
            OnChangeState?.Invoke(State.BallKicked);
            _ballModel.BallRenderer.material.color = _colorStart;
        }

        public void Cleanup()
        {
            _userInput.OnTouchDown -= OnMouseButtonDown;
            _userInput.OnTouchUp -= OnMouseButtonUp;
            _userInput.OnTouch -= OnMouseButton;
            _userInput.OnChangeMousePosition -= GetMousePosition;
        }

        public void ChangeState(State state)
        {
            if (state == State.Start)
            {
                
            }
        }
    }
}

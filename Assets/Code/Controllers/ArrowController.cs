using System;
using System.Collections.Generic;
using Code.Arrow;
using Code.Ball;
using Code.GameState;
using Code.Interfaces;
using Code.UserInput;
using UnityEngine;

namespace Code.Controllers
{
    internal sealed class ArrowController : IInitialize, IExecute, ICleanup, IState
    {
        public event Action<State> OnChangeState;
        private readonly IUserInput _userInput;
        private readonly ArrowDirection _arrowDirection;
        private readonly ArrowColor _arrowColor;
        private State _state;
        private Vector3 _mousePosition;
        private bool _isMouseButton;

        public ArrowController(Transform arrow, IUserInput input, Transform ball, IForceModel speedModel,
            List<Material> colors)
        {
            _userInput = input;
            _arrowDirection = new ArrowDirection(arrow, ball);
            _arrowColor = new ArrowColor(arrow, speedModel, colors);
        }

        public void Initialize()
        {
            _userInput.OnTouch += OnMouseButton;
            _userInput.OnChangeMousePosition += GetMousePosition;
        }

        public void ChangeState(State state)
        {
            _state = state;
            switch (state)
            {
                case State.Start:
                    break;
                case State.BallTouched:
                    _arrowDirection.Run(_mousePosition);
                    _arrowColor.RunningColorChanges();
                    break;
                case State.BallKicked:
                    _arrowColor.RemoveColorChanges();
                    _arrowDirection.TurnOff();
                    break;
                case State.Victory:
                    break;
                case State.Defeat:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void GetMousePosition(Vector3 position) => _mousePosition = position;
        private void OnMouseButton(bool value) => _isMouseButton = value;

        public void Execute(float deltaTime)
        {
            if (_state == State.BallTouched && _isMouseButton)
            {
                _arrowDirection.FollowDirection(_mousePosition);
            }
        }

        public void Cleanup()
        {
            _userInput.OnChangeMousePosition -= GetMousePosition;
            _userInput.OnTouch -= OnMouseButton;
        }
    }
}

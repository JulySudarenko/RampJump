using System;
using Code.Interfaces;
using Code.UniversalFactory;
using Code.Assistant;
using Code.GameState;
using UnityEngine;

namespace Code.Controllers
{
    internal class GameplayController : IInitialize, IExecute, ICleanup, IState
    {
        public event Action<State> OnChangeState;

        private const float MAX_STOP_DURATION = 50.0f;
        private const float DISTANCE_TRASH = 0.005f;

        private readonly Transform _ball;
        private readonly Transform _hole;
        private readonly int _ballID;

        private TriggerContacts _contacts;
        private State _state;
        private Vector3 _previousPosition;
        private float _counter = 0.0f;


        public GameplayController(Transform hole, Transform ballModel, int ballID)
        {
            _ball = ballModel;
            _hole = hole;
            _ballID = ballID;
        }

        public void Initialize()
        {
            _previousPosition = _ball.position;
            _contacts = HelperExtentions.GetOrAddComponent<TriggerContacts>(_hole.gameObject);
            _contacts.IsContact += CheckHoleContact;
        }

        public void ChangeState(State state) => _state = state;


        public void Execute(float deltaTime)
        {
            if (_state == State.BallKicked)
            {
                CheckBallSpeed();
            }
        }

        private void CheckHoleContact(int hitID, int objID)
        {
            if (hitID == _ballID)
            {
                OnChangeState?.Invoke(State.Victory);
            }
        }

        private void CheckBallSpeed()
        {
            if (Mathf.Abs(_ball.position.x - _previousPosition.x) < DISTANCE_TRASH
                && Mathf.Abs(_ball.position.y - _previousPosition.y) < DISTANCE_TRASH)
            {
                _counter++;
                _previousPosition = _ball.position;
                CheckStopPeriod();
            }
            else
            {
                _counter = 0.0f;
                _previousPosition = _ball.position;
            }
        }

        private void CheckStopPeriod()
        {
            if (_counter >= MAX_STOP_DURATION)
            {
                OnChangeState?.Invoke(State.Defeat);
            }
        }

        public void Cleanup()
        {
            _contacts.IsContact -= CheckHoleContact;
        }
    }
}

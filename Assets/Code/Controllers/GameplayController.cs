using System;
using Code.Interfaces;
using Code.Models;
using Code.UniversalFactory;
using Code.Assistant;
using UnityEngine;

namespace Code.Controllers
{
    internal class GameplayController : IInitialize, IExecute, ICleanup, IFinishEvents
    {
        public event Action OnVictory;
        public event Action OnDefeat;
        private readonly IBallEvents _ballEvents;
        private readonly IBallModel _ballModel;
        private readonly Transform _hole;
        private TriggerContacts _contacts;
        private Vector3 _previousPosition;
        private float _counter = 0.0f;
        private float _distanceTrash = 0.005f;
        private readonly float _maxStopDuration = 50.0f;
        private bool _isBallKicked = false;

        public GameplayController(IBallEvents ballEvents, Transform hole, IBallModel ballModel)
        {
            _ballModel = ballModel;
            _hole = hole;
            _ballEvents = ballEvents;
        }

        public void Initialize()
        {
            _previousPosition = _ballModel.Ball.position;
            var colliderChild = _hole.GetComponentInChildren<CapsuleCollider>();
            _contacts = HelperExtentions.GetOrAddComponent<TriggerContacts>(colliderChild.gameObject);
            _contacts.IsStayContact += CheckHoleContact;
            _ballEvents.OnBallKicked += OnBallKickedChange;
        }

        private void OnBallKickedChange(bool value) => _isBallKicked = value;

        public void Execute(float deltaTime)
        {
            if (_isBallKicked)
            {
                CheckBallSpeed();
                CheckStopPeriod();
            }
        }

        private void CheckHoleContact(int hitID)
        {
            if (hitID == _ballModel.BallID)
            {
                OnVictory?.Invoke();
                _isBallKicked = false;
            }
        }

        private void CheckBallSpeed()
        {
            if (Mathf.Abs(_ballModel.Ball.position.x - _previousPosition.x) < _distanceTrash
                && Mathf.Abs(_ballModel.Ball.position.y - _previousPosition.y) < _distanceTrash)
            {
                _counter++;
                _previousPosition = _ballModel.Ball.position;
            }
            else
            {
                _counter = 0.0f;
                _previousPosition = _ballModel.Ball.position;
            }
        }

        private void CheckStopPeriod()
        {
            if (_counter >= _maxStopDuration)
            {
                OnDefeat?.Invoke();
                _isBallKicked = false;
            }
        }

        public void Cleanup()
        {
            _contacts.IsStayContact -= CheckHoleContact;
            _ballEvents.OnBallKicked -= OnBallKickedChange;
        }
    }
}

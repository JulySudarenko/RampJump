﻿using Code.Assistant;
using Code.Ball;
using Code.UniversalFactory;
using UnityEngine;

namespace Code.Controllers
{
    internal class BallLandingController
    {
        private const int STOP_FACTOR = 5;
        private readonly IBall _ball;
        private readonly Hit _hit;

        public BallLandingController(Component bottom, IBall ball)
        {
            _ball = ball;
            var colliderChild = bottom.GetComponentInChildren<Collider>();
            _hit = colliderChild.gameObject.GetOrAddComponent<Hit>();
        }

        public void Init()
        {
            _hit.OnHit += OnBottomHit;
        }

        private void OnBottomHit(int collisionID, int objID)
        {
            if (collisionID == _ball.BallTransform.gameObject.GetInstanceID())
            {
                _ball.BallRigidbody.velocity /= STOP_FACTOR;
                _ball.BallRigidbody.angularVelocity /= STOP_FACTOR;
            }
        }

        public void Cleanup()
        {
            _hit.OnHit -= OnBottomHit;
        }
    }
}

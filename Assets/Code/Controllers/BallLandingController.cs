using Code.Assistant;
using Code.Interfaces;
using Code.Models;
using Code.UniversalFactory;
using UnityEngine;

namespace Code.Controllers
{
    internal class BallLandingController
    {
        private readonly IBallModel _ballModel;
        private readonly Hit _hit;

        public BallLandingController(Component bottom, IBallModel ball)
        {
            _ballModel = ball;
            var colliderChild = bottom.GetComponentInChildren<Collider>();
            _hit = colliderChild.gameObject.GetOrAddComponent<Hit>();
        }

        public void Init()
        {
            _hit.OnHit += OnBottomHit;
        }

        private void OnBottomHit(int collisionID, int objID)
        {
            if (collisionID == _ballModel.Ball.gameObject.GetInstanceID())
            {
                _ballModel.BallRigidbody.velocity = _ballModel.BallRigidbody.velocity / 2;
                _ballModel.BallRigidbody.angularVelocity = _ballModel.BallRigidbody.angularVelocity / 2;
            }
        }

        public void Cleanup()
        {
            _hit.OnHit -= OnBottomHit;
        }
    }
}

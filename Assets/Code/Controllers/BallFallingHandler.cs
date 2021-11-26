using Code.Assistant;
using Code.Models;
using Code.UniversalFactory;
using UnityEngine;

namespace Code.Controllers
{
    internal class BallFallingHandler
    {
        private readonly IBallModel _ballModel;
        private readonly Hit _hit;

        public BallFallingHandler(Transform bottom, IBallModel ball)
        {
            _ballModel = ball;
            _hit = HelperExtentions.GetOrAddComponent<Hit>(bottom.gameObject);
        }

        public void Init()
        {
            _hit.OnHit += OnBottomHit;
        }

        private void OnBottomHit(int colliderID)
        {
            if (colliderID == _ballModel.Ball.gameObject.GetInstanceID())
            {
                _ballModel.BallRigidbody.velocity = Vector3.zero;
                _ballModel.BallRigidbody.angularVelocity = Vector3.zero;
            }
        }

        public void Cleanup()
        {
            _hit.OnHit -= OnBottomHit;
        }
    }
}

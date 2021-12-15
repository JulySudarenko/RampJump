using Code.Configs;
using Code.UniversalFactory;
using UnityEngine;

namespace Code.Models
{
    public class BallModel : IBallModel
    {
        public Transform Ball { get; }
        public Rigidbody BallRigidbody { get; }
        public Renderer BallRenderer { get; }
        public SphereCollider BallCollider { get; }
        public int BallID { get; }

        public BallModel(ActiveObjectConfig config)
        {
            Ball = new ObjectInitialization(new Factory(config.BallPrefab)).Create();
            BallRigidbody = Ball.GetComponentInChildren<Rigidbody>();
            BallCollider = Ball.GetComponentInChildren<SphereCollider>();
            BallRenderer = Ball.GetComponentInChildren<Renderer>();
            BallID = BallCollider.gameObject.GetInstanceID();
            
        }
    }
}

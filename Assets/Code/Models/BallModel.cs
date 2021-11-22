using Code.Configs;
using Code.UniversalFactory;
using UnityEngine;

namespace Code.Models
{
    public class BallModel : IBallModel
    {
        public Transform Ball { get; }
        public Rigidbody BallRigidbody { get; }
        public SphereCollider BallCollider { get; }
        public float BallSpeed { get; }
        public int BallID { get; }

        public BallModel(ActiveObjectConfig config)
        {
            BallSpeed = config.Speed;
            Ball = new ObjectInitialization(new Factory(config.Prefab)).Create();
            BallRigidbody = Ball.GetComponentInChildren<Rigidbody>(Ball.gameObject);
            BallCollider = Ball.GetComponentInChildren<SphereCollider>();
            BallID = BallCollider.gameObject.GetInstanceID();
        }
    }
}

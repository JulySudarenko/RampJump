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

        public BallModel(ActiveObjectConfig config, Transform folder)
        {
            Ball = new ObjectInitialization(new Factory(config.BallPrefab, folder)).Create();
            BallRigidbody = Ball.GetComponentInChildren<Rigidbody>();
            BallCollider = Ball.GetComponentInChildren<SphereCollider>();
            BallRenderer = Ball.GetComponentInChildren<Renderer>();
            BallID = BallCollider.gameObject.GetInstanceID();
        }
    }
}

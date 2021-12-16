using Code.Assistant;
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
        public Hit BallHit { get; }
        public AudioSource AudioSource { get; }
        public int BallID { get; }

        public BallModel(ActiveObjectConfig config)
        {
            Ball = new ObjectInitialization(new Factory(config.BallPrefab)).Create();
            BallRigidbody = Ball.GetComponentInChildren<Rigidbody>();
            BallRenderer = Ball.GetComponentInChildren<Renderer>();
            AudioSource = Ball.gameObject.GetOrAddComponent<AudioSource>();
            var ballCollider = Ball.GetComponentInChildren<SphereCollider>();
            BallHit = ballCollider.gameObject.GetOrAddComponent<Hit>();
            BallID = ballCollider.gameObject.GetInstanceID();
        }
    }
}

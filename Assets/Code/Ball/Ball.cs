using Code.Assistant;
using Code.Configs;
using Code.UniversalFactory;
using UnityEngine;

namespace Code.Ball
{
    public class Ball : IBall
    {
        public Transform BallTransform { get; }
        public Rigidbody BallRigidbody { get; }
        public Renderer BallRenderer { get; }
        public Hit BallHit { get; }
        public AudioSource BallAudioSource { get; }
        public int BallID { get; }

        public Ball(ActiveObjectConfig config)
        {
            BallTransform = new ObjectInitialization(new Factory(config.BallPrefab)).Create();
            BallRigidbody = BallTransform.GetComponentInChildren<Rigidbody>();
            BallRenderer = BallTransform.GetComponentInChildren<Renderer>();
            BallAudioSource = BallTransform.gameObject.GetOrAddComponent<AudioSource>();
            var ballCollider = BallTransform.GetComponentInChildren<SphereCollider>();
            BallHit = ballCollider.gameObject.GetOrAddComponent<Hit>();
            BallID = ballCollider.gameObject.GetInstanceID();
        }
    }
}

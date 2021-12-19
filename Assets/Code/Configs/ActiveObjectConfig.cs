using Code.Models;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "ActiveObjectConfig", menuName = "Configs/ActiveObjectConfig", order = 0)]
    public class ActiveObjectConfig : ScriptableObject, IForceModel
    {
        [Header("Prefabs")]
        public Transform BallPrefab;
        public Transform ArrowPrefab;
        public Transform HolePrefab;

        [Header("Sounds")] 
        public AudioClip SlimeSound;
        public AudioClip CoinSound;
        public AudioClip KickSound;
        public AudioClip DirectHitSound;
        
        [Header("Speed settings")]
        [SerializeField, Range(0, 2000)] private float _force;
        [SerializeField, Range(0, 500)] private float _forceRiseFactor = 50.0f;
        [SerializeField, Range(0, 50)] private float _colorRiseFactor = 5.0f;

        [Header("Coins settings")] 
        [SerializeField, Range(0, 180)] private float _coinsRotationAngle = 70.0f;
        [SerializeField, Range(0, 10)] private float _coinsRotationSpeed = 4.0f;
        
        public float BallForce => _force;
        public float ForceRiseFactor => _forceRiseFactor;
        public float ColorRiseFactor => _colorRiseFactor;
        public float CoinsRotationAngle => _coinsRotationAngle;
        public float CoinsRotationSpeed => _coinsRotationSpeed;
    }
}

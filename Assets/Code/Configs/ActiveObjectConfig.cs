using Code.Models;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "ActiveObjectConfig", menuName = "Configs/ActiveObjectConfig", order = 0)]
    public class ActiveObjectConfig : ScriptableObject, IBallForceModel
    {
        public Transform BallPrefab;
        public Transform ArrowPrefab;
        public Transform HolePrefab;
        [SerializeField, Range(0, 2000)] private float _force;
        [SerializeField, Range(0, 500)] private float _forceRiseFactor = 50.0f;
        [SerializeField, Range(0, 50)] private float _colorRiseFactor = 5.0f;

        public float BallForce => _force;
        public float ForceRiseFactor => _forceRiseFactor;
        public float ColorRiseFactor => _colorRiseFactor;
    }
}

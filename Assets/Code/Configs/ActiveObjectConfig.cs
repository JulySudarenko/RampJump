using Code.Assistant;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "ActiveObjectConfig", menuName = "Configs/ActiveObjectConfig", order = 0)]
    internal class ActiveObjectConfig : ScriptableObject
    {
        public Transform Prefab;
        [SerializeField] private float _speed;
        [SerializeField] private ActiveObjectsName _name;

        public float Speed => _speed;
        public ActiveObjectsName Name => _name;
    }
}

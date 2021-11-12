using Code.Assistant;
using Code.Interfaces;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "ActiveObjectConfig", menuName = "Configs/ActiveObjectConfig", order = 0)]
    internal class ActiveObjectConfig : ScriptableObject, IActiveObject
    {
        public GameObject Prefab;
        [SerializeField] private float _speed;
        [SerializeField] private ActiveObjectsName _name;

        public GameObject ActiveObjectPrefab => Prefab;
        public float Speed => _speed;
        public ActiveObjectsName Name => _name;
    }
}

using System.Collections.Generic;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "LevelObjectConfig", menuName = "Configs/LevelObjectConfig", order = 0)]
    internal class LevelObjectConfig : ScriptableObject
    {
        public List<Transform> LevelDetailsPrefabs;
        public Transform BottomPrefab;
        public Transform BallStartPlace;
        public Transform BallStartPosition;
        public Transform HoleStartPosition;
        [SerializeField] private int _levelNumber;

        public int LevelNumber => _levelNumber;
    }
}

using System.Collections.Generic;
using Code.Interfaces;
using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "LevelObjectConfig", menuName = "Configs/LevelObjectConfig", order = 0)]
    internal class LevelObjectConfig : ScriptableObject, ILevelObjects, IStartPositions
    {
        public List<GameObject> LevelDetailsPrefabs;
        [SerializeField] private Transform _ballStartPosition;
        [SerializeField] private Transform _glassStartPosition;
        [SerializeField] private int _levelNumber;

        public List<GameObject> LevelDetails => LevelDetailsPrefabs;
        public Transform BallStartPosition => _ballStartPosition;
        public Transform GlassStartPosition => _glassStartPosition;
        public int LevelNumber => _levelNumber;
    }
}

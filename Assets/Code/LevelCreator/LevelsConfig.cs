using System.Collections.Generic;
using UnityEngine;

namespace Code.LevelCreator
{
    [CreateAssetMenu(fileName = "LevelsConfig", menuName = "Configs/LevelsConfig", order = 0)]
    public class LevelsConfig : ScriptableObject
    {
        public DetailsBaseConfig DetailsBase;
        public List<DetailCreatingInfo> CreatingDetails = new List<DetailCreatingInfo>();
    }
}

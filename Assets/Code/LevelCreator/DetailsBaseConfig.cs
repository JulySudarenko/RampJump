using System.Collections.Generic;
using UnityEngine;

namespace Code.LevelCreator
{
    [CreateAssetMenu(fileName = "DetailsBaseConfig", menuName = "Configs/DetailsBaseConfig", order = 0)]
    public class DetailsBaseConfig : ScriptableObject
    {
        public List<Transform> DetailBase;
    }
}

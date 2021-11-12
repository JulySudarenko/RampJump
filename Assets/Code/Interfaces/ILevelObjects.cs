using System.Collections.Generic;
using UnityEngine;

namespace Code.Interfaces
{
    internal interface ILevelObjects
    {
        List<GameObject> LevelDetails { get; }
        int LevelNumber { get; }
    }
}

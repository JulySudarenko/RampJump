using UnityEngine;

namespace Code.UniversalFactory
{
    internal interface ILevel
    {
        Transform BallStartPlace { get; }
        Vector3 BallStartPosition { get; }
        Vector3 HoleStartPosition { get; }
        int TotalLevels { get; }
        void InitNewLevel(int levelNumber);
    }
}

using UnityEngine;

namespace Code.LevelConstructor
{
    internal interface ILevel
    {
        Transform Bottom { get; }
        Transform BallStartPlace { get; }
        Vector3 BallStartPosition { get; }
        Vector3 HoleStartPosition { get; }
        int TotalLevels { get; }
        void InitNewLevel(int levelNumber);
    }
}

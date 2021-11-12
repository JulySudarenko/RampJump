using UnityEngine;

namespace Code.Interfaces
{
    internal interface IStartPositions
    {
        Transform BallStartPosition { get; }
        Transform GlassStartPosition { get; }
    }
}

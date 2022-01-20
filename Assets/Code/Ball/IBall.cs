using Code.UniversalFactory;
using UnityEngine;

namespace Code.Ball
{
    public interface IBall
    {
        Transform BallTransform { get; }
        Rigidbody BallRigidbody { get; }
        Hit BallHit { get; }
        Renderer BallRenderer { get; }
        AudioSource BallAudioSource { get; }
        int BallID { get; }
    }
}

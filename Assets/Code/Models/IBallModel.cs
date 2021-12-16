using Code.UniversalFactory;
using UnityEngine;

namespace Code.Models
{
    public interface IBallModel
    {
        Transform Ball { get; }
        Rigidbody BallRigidbody { get; }
        Hit BallHit { get; }
        Renderer BallRenderer { get; }
        AudioSource AudioSource { get; }
        int BallID { get; }
    }
}

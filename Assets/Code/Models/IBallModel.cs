using UnityEngine;

namespace Code.Models
{
    public interface IBallModel
    {
        Transform Ball { get; }
        Rigidbody BallRigidbody { get; }
        SphereCollider BallCollider { get; }
        float BallSpeed { get; }
        int BallID { get; }
    }
}

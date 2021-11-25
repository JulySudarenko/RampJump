using UnityEngine;

namespace Code.Models
{
    public interface IBallModel
    {
        Transform Ball { get; }
        Rigidbody BallRigidbody { get; }
        SphereCollider BallCollider { get; }
        Renderer BallRenderer { get; }
        int BallID { get; }
    }
}

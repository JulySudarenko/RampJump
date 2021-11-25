namespace Code.Models
{
    public interface IBallForceModel
    {
        float BallForce { get; }
        float ForceRiseFactor { get; }
        float ColorRiseFactor { get; }
    }
}

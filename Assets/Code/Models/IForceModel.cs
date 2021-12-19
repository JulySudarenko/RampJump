namespace Code.Models
{
    public interface IForceModel
    {
        float BallForce { get; }
        float ForceRiseFactor { get; }
        float ColorRiseFactor { get; }
    }
}

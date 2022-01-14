namespace Code.Ball
{
    public interface IForceModel
    {
        float BallForce { get; }
        float ForceRiseFactor { get; }
        float ColorRiseFactor { get; }
    }
}

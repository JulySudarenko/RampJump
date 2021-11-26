namespace Code.Interfaces
{
    internal interface IFixedExecute : IController
    {
        void FixedExecute(float deltaTime);
    }
}

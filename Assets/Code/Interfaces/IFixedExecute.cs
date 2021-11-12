using Code.Interfaces;

namespace Code.UserInput
{
    internal interface IFixedExecute : IController
    {
        void FixedExecute(float deltaTime);
    }
}

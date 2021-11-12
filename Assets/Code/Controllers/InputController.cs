using Code.UserInput;
using RampJump;

namespace Code.Controllers
{
    public sealed class InputController : IFixedExecute
    {
        private readonly ITouchInput _touchInput;

        public InputController(ITouchInput touchInput)
        {
            _touchInput = touchInput;
        }
        
        public void FixedExecute(float deltaTime)
        {
            _touchInput.GetTouchDown();
            _touchInput.GetTouchUp();
        }
    }
}

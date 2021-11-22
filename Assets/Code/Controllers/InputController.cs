using Code.Interfaces;
using Code.UserInput;

namespace Code.Controllers
{
    public sealed class InputController : IFixedExecute
    {
        private readonly IUserInput _userInput;

        public InputController(IUserInput userInput)
        {
            _userInput = userInput;
        }
        
        public void FixedExecute(float deltaTime)
        {
            _userInput.GetTouchDown();
            _userInput.GetTouchUp();
            _userInput.GetTouch();
        }
    }
}

using System;
using RampJump;
using UnityEngine;

namespace Code.UserInput
{
    public class TouchInputHandling : ITouchInput
    {
        public event Action<bool> OnTouchDown = delegate(bool b) {  };
        public event Action<bool> OnTouchUp = delegate(bool b) {  };
        public event Action<Vector2> OnTouch = delegate(Vector2 vector) {  };

        public void GetTouchDown()
        {
            OnTouchDown?.Invoke(Input.GetMouseButtonDown(0));
            OnTouch?.Invoke(Input.mousePosition);
        }

        public void GetTouchUp()
        {
            OnTouchUp?.Invoke(Input.GetMouseButtonUp(0));
            OnTouch?.Invoke(Input.mousePosition);
        }
    }
}

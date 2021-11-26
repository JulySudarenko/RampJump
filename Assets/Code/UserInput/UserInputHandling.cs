using System;
using UnityEngine;

namespace Code.UserInput
{
    internal class UserInputHandling : IUserInput
    {
        public event Action<bool> OnTouchDown = delegate(bool b) { };
        public event Action<bool> OnTouchUp = delegate(bool b) { };
        public event Action<bool> OnTouch = delegate(bool b) { };
        public event Action<Vector3> OnChangeMousePosition = delegate(Vector3 vector) { };

        public void GetTouchDown()
        {
            OnTouchDown?.Invoke(Input.GetMouseButtonDown(0));
            OnChangeMousePosition?.Invoke(Input.mousePosition);
        }

        public void GetTouchUp()
        {
            OnTouchUp?.Invoke(Input.GetMouseButtonUp(0));
            OnChangeMousePosition?.Invoke(Input.mousePosition);
        }

        public void GetTouch()
        {
            OnTouch?.Invoke(Input.GetMouseButton(0));
            OnChangeMousePosition?.Invoke(Input.mousePosition);
        }
    }
}

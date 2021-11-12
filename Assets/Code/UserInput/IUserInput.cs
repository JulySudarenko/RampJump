using System;
using UnityEngine;

namespace Code.UserInput
{
    public interface IUserInput
    {
        event Action<bool> OnTouchDown;
        event Action<bool> OnTouchUp;
        event Action<bool> OnTouch;
        event Action<Vector3> OnChangeMousePosition;

        void GetTouchDown();
        void GetTouchUp();
        void GetTouch();
    }
}

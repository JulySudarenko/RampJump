using System;
using UnityEngine;

namespace RampJump
{
    public interface ITouchInput
    {
        event Action<bool> OnTouchDown;
        event Action<bool> OnTouchUp;
        event Action<Vector2> OnTouch;
        
        void GetTouchDown();
        void GetTouchUp();
    }
}

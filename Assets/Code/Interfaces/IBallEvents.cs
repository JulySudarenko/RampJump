using System;

namespace Code.Interfaces
{
    public interface IBallEvents
    {
        event Action<bool> OnBallTouched;
        event Action<bool> OnBallKicked;
    }
}

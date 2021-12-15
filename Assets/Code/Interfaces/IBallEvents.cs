using System;
using Code.Controllers;
using Code.GameState;

namespace Code.Interfaces
{
    public interface IBallEvents
    {
        event Action<State> OnBallTouched;
        event Action<State> OnBallKicked;
    }
}

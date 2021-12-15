using System;
using Code.Controllers;
using Code.GameState;

namespace Code.Interfaces
{
    internal interface IFinishEvents
    {
        event Action<State> OnVictory;
        event Action<State> OnDefeat;
    }
}

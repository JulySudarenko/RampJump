using System;

namespace Code.Interfaces
{
    internal interface IFinishEvents
    {
        event Action OnVictory;
        event Action OnDefeat;
    }
}

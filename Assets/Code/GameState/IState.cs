using System;

namespace Code.GameState
{
    public interface IState
    {
        event Action<State> OnChangeState;
        void ChangeState(State state);
    }
}

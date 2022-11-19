using System;

namespace StateMachine
{
    public interface IStateMachine<TState> where TState : AbstractState
    {
        void AddState(TState state);
        void SetStartState(string stateName);
        void SetTransition(string from, string to, string[] inputs);
        TState CurrentState { get; }
        event Action OnStateChanged;
    }
}
using System;
using System.Collections.Generic;

namespace StateMachine
{
    public class StateMachine<TState> : IStateMachine<TState> where TState : AbstractState
    {
        public event Action OnStateChanged;

        protected Dictionary<string, TState> allStates = new Dictionary<string, TState>();
        
        public TState CurrentState { get; private set; }

        public void AddState(TState state)
        {
            allStates[state.StateId] = state;
        }

        public void SetStartState(string stateName)
        {
            SetNewCurrentStateAndDoBind(allStates[stateName]);
            CurrentState.OnEnter();
        }

        public void SetTransition(string from, string to, string[] inputs)
        {
            allStates[from].CreateTransition(to, inputs);
        }

        protected virtual void CheckIfCurrentStateHasTransition(string input)
        {
            string stateToTransition = CurrentState.GetStateToTransitionIfInputAvailable(input);
            bool canTransition = stateToTransition != null;

            if (canTransition)
            {
                ChangeState(allStates[stateToTransition]);
            }
        }
        
        private void ChangeState(TState newState)
        {
            if (CurrentState.StateId != newState.StateId)
            {
                AbstractStateTransitionTransferData stateTransitionTransferData = CurrentState.GetTransferData();
                CallOnExitAndUnbindCurrentState();
                SetNewCurrentStateAndDoBind(newState);

                CurrentState.DeliverPreviousStateTransferData(stateTransitionTransferData);
                CurrentState.OnEnter();
                OnStateChanged?.Invoke();
            }
        }

        private void CallOnExitAndUnbindCurrentState()
        {
            CurrentState.OnExit();
            CurrentState.OnCheckIfHasTransition -= CheckIfCurrentStateHasTransition;
        }

        private void SetNewCurrentStateAndDoBind(TState newState)
        {
            CurrentState = newState;

            CurrentState.OnCheckIfHasTransition += CheckIfCurrentStateHasTransition;
        }
    }
}

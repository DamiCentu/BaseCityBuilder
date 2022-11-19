using System;
using System.Collections.Generic;

namespace StateMachine
{
    public abstract class AbstractState
    {
        public virtual event Action<string> OnCheckIfHasTransition;

        protected readonly List<StateTransition> availableTransitions = new List<StateTransition>();

        protected AbstractState(string stateId)
        {
            StateId = stateId;
        }

        public string StateId { get; private set; }

        public virtual void OnEnter() { }

        public virtual void OnUpdate() { }
        public virtual void OnExit() { }

        public virtual void CreateTransition(string stateDestiny, string[] inputs)
        {
            StateTransition transition = new StateTransition()
            {
                stateDestinyId = stateDestiny,
                inputs = inputs,
            };

            availableTransitions.Add(transition);
        }

        public virtual string GetStateToTransitionIfInputAvailable(string input)
        {
            foreach (StateTransition transition in availableTransitions)
            {
                if (transition.ListensToInput(input))
                {
                    return transition.stateDestinyId;
                }
            }

            return null;
        }

        public abstract AbstractStateTransitionTransferData GetTransferData();

        public abstract void DeliverPreviousStateTransferData(AbstractStateTransitionTransferData stateTransitionTransferData);

        protected void DispatchOnCallTransition(string value)
        {
            OnCheckIfHasTransition?.Invoke(value);
        }
    }
}

using System.Linq;

namespace StateMachine
{
    public class StateTransition
    {
        public string stateDestinyId;
        public string[] inputs;

        public bool ListensToInput(string input) => inputs.Contains(input);
    }
}
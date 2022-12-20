using StateMachine;

namespace Graph
{
    public class GridNodeState : AbstractState
    {
        public GridNodeState(string stateId) : base(stateId) { }

        public override AbstractStateTransitionTransferData GetTransferData()
        {
            throw new System.NotImplementedException();
        }

        public override void DeliverPreviousStateTransferData(AbstractStateTransitionTransferData stateTransitionTransferData)
        {
            throw new System.NotImplementedException();
        }
    }
}
using Settable;

namespace Graph
{
    public class GridNode : AbstractGraphNode, ISettable
    {

        public void Setup(AbstractSetupConfig setupConfig)
        {
            GridNodeSetupConfig configCasted = setupConfig as GridNodeSetupConfig;
            
            GridCoordinates = configCasted.Coordinates;
        }
        
        public GridCoordinates GridCoordinates { get; private set; }
    }

    public class GridNodeSetupConfig : AbstractSetupConfig
    {
        public GridCoordinates Coordinates;
    }
}
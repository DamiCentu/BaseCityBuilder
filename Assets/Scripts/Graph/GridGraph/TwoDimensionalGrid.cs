using Settable;
using UnityEngine;

namespace Graph
{
    public class TwoDimensionalGrid : AbstractGrid<AbstractGraphNode[,]>, ISettable
    {
        public void Setup(AbstractSetupConfig setupConfig)
        {
            GridSetupConfig configCasted = setupConfig as GridSetupConfig;
            
            AllNodes = new AbstractGraphNode[configCasted.gridSize.x, configCasted.gridSize.y];
        }

        public override AbstractGraphNode GetNodeAtCoordinate(GridCoordinates coordinates)
        {
            return AllNodes[coordinates.X, coordinates.Y];
        }
    }
}
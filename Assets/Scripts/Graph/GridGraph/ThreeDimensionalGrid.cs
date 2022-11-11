using Settable;

namespace Graph
{
    public class ThreeDimensionalGrid<TNode> : AbstractGrid<TNode[,,]>, ISettable where TNode : GridNode, new()
    {
        public void Setup(AbstractSetupConfig setupConfig)
        {
            GridSetupConfig configCasted = setupConfig as GridSetupConfig;
            
            AllNodes = new TNode[configCasted.gridSize.x, configCasted.gridSize.y, configCasted.gridSize.z];

            Factory = configCasted.Factory;

            CreateGridNodes();
        }

        private void CreateGridNodes()
        {
            for (int x = 0; x < AllNodes.GetLength((int)Dimensions.X); x++)
            {
                for (int y = 0; y < AllNodes.GetLength((int)Dimensions.Y); y++)
                {
                    for (int z = 0; z < AllNodes.GetLength((int)Dimensions.Z); z++)
                    {
                        TNode currentNode = Factory.CreateInstance<TNode>();
                        currentNode.Setup(new GridNode.GridNodeSetupConfig()
                        {
                            Coordinates = new GridCoordinates(x, y, z)
                        }); 
                        
                        AllNodes[x, y, z] = currentNode;
                    }
                }
            }
        }

        public override AbstractGraphNode GetNodeAtCoordinate(GridCoordinates coordinates) => AllNodes[coordinates.X, coordinates.Y, coordinates.Y];
        
    }
    
}
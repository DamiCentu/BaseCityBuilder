using Graph;
using UnityEngine;
using Factory;

public class GridTest : MonoBehaviour
{
    [SerializeField] private Vector3Int size;
    [SerializeField] private GameObject prefab;
    private ThreeDimensionalGrid<GridNode> gameGrid;
    
    void Start()
    {
        gameGrid = new ThreeDimensionalGrid<GridNode>();
        
        GridSetupConfig setupConfig = new GridSetupConfig()
        {
            gridSize = size,
            Factory = new ObjectFactory()
        };
        
        gameGrid.Setup(setupConfig);
    }
}

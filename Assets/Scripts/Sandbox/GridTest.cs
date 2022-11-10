using Graph;
using UnityEngine;
using Presenters;

public class GridTest : MonoBehaviour
{
    [SerializeField] private Vector3Int size;
    [SerializeField] private BaseGridNodePresenter prefab;
    private ThreeDimensionalGrid<GridNode> gameGrid;
    
    void Start()
    {
        gameGrid = new ThreeDimensionalGrid<GridNode>();
        
        GridSetupConfig setupGridConfig = new GridSetupConfig()
        {
            gridSize = size
        };
        
        gameGrid.Setup(setupGridConfig);

        //TODO move responsability
        foreach (GridNode node in gameGrid.AllNodes)
        {
            BaseGridNodeModel baseGridNodeModel = new BaseGridNodeModel();
            baseGridNodeModel.Setup(new BaseGridNodeModelSetupConfig()
            {
                GridNode = node
            });

            BaseGridNodePresenter nodePresenter = Instantiate(prefab, transform);

            nodePresenter.Model = baseGridNodeModel;
            nodePresenter.ConstructPresenter();
            nodePresenter.Bind();
            nodePresenter.transform.position = GridCoordinates.ToVector3(node.GridCoordinates);
        }
    }
}

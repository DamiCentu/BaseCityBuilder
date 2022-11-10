using UnityEngine;

namespace Presenters
{
    public class BaseGridNodePresenter : AbstractPresenter<BaseGridNodeModel, BaseGridNodeView>
    {
        [SerializeField] private BaseGridNodeView viewPrefab; //todo load from the model
        
        public override void ConstructPresenter()
        {
            View = Instantiate(viewPrefab, transform);
        }
    }
}
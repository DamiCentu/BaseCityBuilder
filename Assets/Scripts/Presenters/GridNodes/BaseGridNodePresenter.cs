using UnityEngine;

namespace Presenters
{
    public class BaseGridNodePresenter : AbstractPresenter<BaseGridNodeModel, BaseGridNodeView>
    {
        [SerializeField] private BaseGridNodeView viewPrefab; //todo load from the model
        
        public override void ConstructPresenter()
        {
            ConstructView();
        }

        private void ConstructView()
        {
            View = ViewFactory.CreateInstance(viewPrefab);
            View.transform.SetParent(transform);
        }
    }
}
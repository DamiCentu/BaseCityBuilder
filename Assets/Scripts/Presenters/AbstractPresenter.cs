using UnityEngine;

public abstract class AbstractPresenter<TModelType, TViewType> : MonoBehaviour, 
                                                                IPresenter<TModelType, TViewType> where TModelType : IModel where TViewType : class, IView
{
    public virtual void Bind() {}
    public virtual void UnBind() {}

    public TViewType View { get; set; }
    public TModelType Model { get; set; }
    public virtual void ConstructPresenter() {}

    protected bool ViewExist => View != null;
}
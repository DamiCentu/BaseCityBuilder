using UnityEngine;

namespace Factory
{
    public class BaseFactory : IFactory
    {
        public TObject CreateInstance<TObject>() where TObject : new() => new TObject();
        public TObject CreateInstance<TObject>(TObject prefab) where TObject : MonoBehaviour => Object.Instantiate(prefab);
    }
}
using UnityEngine;

namespace Factory
{
    public interface IFactory
    {
        public TObject CreateInstance<TObject>() where TObject : new();
        public TObject CreateInstance<TObject>(TObject prefab) where TObject : MonoBehaviour;
    }
}
using UnityEngine;

namespace Factory
{
    public interface IFactory
    {
        public TObject CreateInstance<TObject>() where TObject : new();
    }
}
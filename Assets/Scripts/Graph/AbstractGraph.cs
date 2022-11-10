using System.Collections;

namespace Graph
{
    using Factory;
    
    public abstract class AbstractGraph<TCollection> where TCollection : IEnumerable
    {
        protected IFactory Factory { get; set; }
        public TCollection AllNodes { get; set;}
    }
}

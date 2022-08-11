using System.Collections;

namespace Graph
{
    using Factory;
    
    public abstract class AbstractGraph<TCollection> where TCollection : IEnumerable
    {
        protected IFactory Factory { get; set; }
        protected TCollection AllNodes { get; set;}
    }
}

using System.Collections.Generic;

namespace Graph
{
    public abstract class AbstractGraphNode
    {
        public List<AbstractGraphNode> Adjacents { get; }
    }
}
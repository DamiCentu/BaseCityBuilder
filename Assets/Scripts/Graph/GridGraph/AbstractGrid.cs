using System.Collections;
using Factory;
using Settable;
using UnityEngine;

namespace Graph
{
    public abstract class AbstractGrid<TCollection> : AbstractGraph<TCollection> where TCollection : IEnumerable
    {
        public abstract AbstractGraphNode GetNodeAtCoordinate(GridCoordinates coordinates);
    }
    
    public class GridSetupConfig : AbstractSetupConfig
    {
        public Vector3Int gridSize;
        public IFactory Factory = new Factory.BaseFactory();
    }

    public enum Dimensions
    {
        X = 0,
        Y = 1,
        Z = 2
    }
}
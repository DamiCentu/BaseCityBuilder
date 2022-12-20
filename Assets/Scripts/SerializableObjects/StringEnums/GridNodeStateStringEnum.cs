using System;
using System.Linq;
using Graph;
using Reflection;

namespace SerializableObjects
{
    [Serializable]
    public class GridNodeStateStringEnum : StringEnum
    {
        public override string[] AvailableChoices
        {
            get
            {
                string[] gridNodeStateTypes = 
                    ReflectionUtils.GetImplementationsOf<GridNodeState>()
                    .Select(type => type.Name)
                    .ToArray();
                
                return gridNodeStateTypes;
            }
        }
    }
}
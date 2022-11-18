using System;
using System.Collections.Generic;

namespace DataStructures
{
    public class LookUpTable<Tkey, Tvalue> : ILookUpTable<Tkey, Tvalue>
    {
        private IDictionary<Tkey, Tvalue> table = new Dictionary<Tkey, Tvalue>();
        private Func<Tkey, Tvalue> tValueCreatorMethod;

        public LookUpTable(Func<Tkey, Tvalue> creatorMethod)
        {
            tValueCreatorMethod = creatorMethod;
        }

        public Tvalue GetValue(Tkey key)
        {
            if (table.ContainsKey(key))
            {
                return table[key];
            }
            else
            {
                Tvalue value = tValueCreatorMethod(key);
                table[key] = value;
                return value;
            }
        }

    }
}
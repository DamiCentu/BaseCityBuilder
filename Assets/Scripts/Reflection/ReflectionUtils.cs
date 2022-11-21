using System.Collections.Generic;
using System;
using System.Reflection;

namespace Reflection
{
    public static class ReflectionUtils
    {
        /// <summary>
        /// Returns all the Types that implements a given Class or Interface.
        /// </summary>
        public static List<Type> GetImplementationsOf<T>()
        {
            List<Type> toReturn = new List<Type>();
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (typeof(T).IsAssignableFrom(type))
                    {
                        toReturn.Add(type);
                    }
                }
            }
            return toReturn;
        }
    }
}

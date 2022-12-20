using System.Collections.Generic;
using System;
using System.Reflection;
using DataStructures;

namespace Reflection
{
    public static class ReflectionUtils
    {
        private static LookUpTable<Type, List<Type>> listOfImplementationsTable;
        private static LookUpTable<string, Type> typesTable;
        
        /// <summary>
        /// Returns all the Types that implements a given Class or Interface.
        /// </summary>
        public static List<Type> GetImplementationsOf<T>()
        {
            if (listOfImplementationsTable == null)
            {
                listOfImplementationsTable = new LookUpTable<Type, List<Type>>(GetImplementationsOfCreatorMethod);
            }
            
            return listOfImplementationsTable.GetValue(typeof(T));
        }
        
        public static Type GetType(string className)
        {
            if (typesTable == null)
            {
                typesTable = new LookUpTable<string, Type>(GetTypeCreatorMethod);
            }
            
            return typesTable.GetValue(className);
        }
        
        [UnityEditor.Callbacks.DidReloadScripts]
        private static void OnScriptsReloaded() 
        {
            //Force recalculation.
            listOfImplementationsTable = null; 
            typesTable = null; 
        }

        private static Type GetTypeCreatorMethod(string className)
        {
            Type toReturn = null;
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.Name == className)
                    {
                        toReturn = type;
                    }
                }
            }
            return toReturn;
        }
        
        private static List<Type> GetImplementationsOfCreatorMethod(Type paramType)
        {
            List<Type> toReturn = new List<Type>();
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (paramType.IsAssignableFrom(type))
                    {
                        toReturn.Add(type);
                    }
                }
            }
            return toReturn;
        }
    }
}

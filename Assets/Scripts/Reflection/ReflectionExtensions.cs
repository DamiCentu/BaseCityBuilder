using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace Reflection
{
    public static class ReflectionExtensions
    {
        public static T GetVariableByReflection<T>(this object owner, string variableName,
            BindingFlags bindingFlags = BindingFlags.Default)
        {
            FieldInfo field = owner.GetType().GetField(variableName, bindingFlags);

            if (!(field.FieldType == typeof(T)))
            {
                throw new Exception($"The type of {variableName} doesn't match the type {typeof(T)}");
            }

            return (T)field.GetValue(owner);
        }

        #region Type Extensions

        public static bool ContainsAttribute<T>(this Type owner) => Attribute.IsDefined(owner, typeof(T));

        #endregion


        #region MemberInfo Extensions

        public static bool ContainsAttribute<T>(this MemberInfo owner) => Attribute.IsDefined(owner, typeof(T));

        public static bool ImplementsType<T>(this Type owner)
        {
            return typeof(T).IsAssignableFrom(owner);
        }

        #endregion

        /// <summary>
        /// Creates a clone of an object that contains the [Serializable] attribute.
        /// </summary>
        public static T DeepClone<T>(this T obj)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(memoryStream, obj);
                memoryStream.Position = 0;

                return (T)formatter.Deserialize(memoryStream);
            }
        }
    }
}
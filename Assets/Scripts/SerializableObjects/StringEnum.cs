using System;

namespace SerializableObjects
{
    [Serializable]
    public class StringEnum
    {
        public string CurrentString;
        public virtual string[] AvailableChoices { get; }
    }
}

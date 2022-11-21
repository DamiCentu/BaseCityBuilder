using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace SerializableObjects
{
    [Serializable]
    public class StringEnum
    {
        public string CurrentString;
        public string[] AvailableChoices;

        [SerializeField] public string[] asd { get; set; }
    }
    
    
    [Serializable]
    public class UUU : StringEnum
    {
        [OnSerialized]
        private void OnSerialized()
        {
            Debug.Log("1");
        }
        [OnDeserialized]
        private void OnDeSerialized()
        {
            Debug.Log("2");
        }
    }
}

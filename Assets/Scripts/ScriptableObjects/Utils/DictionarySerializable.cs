using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace ScriptableObjects.Utils
{
    public class DictionarySerializable <TKey, TValue> : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private List<KeyValueSerializable<TKey, TValue>> dictionary = new List<KeyValueSerializable<TKey, TValue>>();

        public Dictionary<TKey, TValue> DeserializedDictionary { get; } = new Dictionary<TKey, TValue>();

        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize()
        {
            DeserializedDictionary.Clear();
            foreach (KeyValueSerializable<TKey, TValue> keyValue in dictionary)
            {
                DeserializedDictionary[keyValue.key] = keyValue.value;
            }
        }
    }
    
    [Serializable]
    public class KeyValueSerializable <TKey, TValue>
    {
        [SerializeField] public TKey key;
        [SerializeField] public TValue value;
    }
}
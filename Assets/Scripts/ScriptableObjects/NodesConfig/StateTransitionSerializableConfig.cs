using System;
using SerializableObjects;
using UnityEngine;

namespace ScriptableObjects
{
    [Serializable]
    public class StateTransitionSerializableConfig <TTransitionStringEnum, TStateStringEnum> where TTransitionStringEnum : StringEnum where TStateStringEnum : StringEnum
    {
        public TTransitionStringEnum[] transitionsInputs;
        [Header("Destiny state")]
        public TStateStringEnum stateDestiny;
    }
}
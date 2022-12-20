using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

[Serializable]
public class StateMachineSerializableConfig
{
    [SerializeField] private List<ScriptableStateConfig> stateMachine = new List<ScriptableStateConfig>();
}

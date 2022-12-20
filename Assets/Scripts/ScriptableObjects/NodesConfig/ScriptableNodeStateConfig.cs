using System.Collections.Generic;
using SerializableObjects;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NodeState", menuName = "ScriptableObject/ScriptableNode/NodeState")]
    public class ScriptableNodeStateConfig : ScriptableStateConfig
    {
        [SerializeField] private GridNodeStateStringEnum stateClassName;
        [SerializeField] private GameObject viewPrefab;
        [SerializeField] private List<StateTransitionSerializableConfig<GridNodeStateStringEnum, GridNodeStateStringEnum>> transitionsConfig;

        public override string StateClassName => stateClassName.CurrentString;
    }
}
using UnityEngine;

[CreateAssetMenu(fileName = "Node", menuName = "ScriptableObject/ScriptableNode/Node")]
public class ScriptableNodeConfig : ScriptableObject
{
     [SerializeField] private string type;
     [SerializeField] private StateMachineSerializableConfig nodeStateMachine = new StateMachineSerializableConfig();
}

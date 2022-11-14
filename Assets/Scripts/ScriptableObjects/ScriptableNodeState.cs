using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NodeState", menuName = "ScriptableObject/ScriptableNode/")]
    public class ScriptableNodeState : ScriptableObject
    {
        [SerializeField] private string stateName;
    }
}
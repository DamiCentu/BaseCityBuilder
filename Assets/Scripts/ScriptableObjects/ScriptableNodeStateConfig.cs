using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NodeState", menuName = "ScriptableObject/ScriptableNode/NodeState")]
    public class ScriptableNodeStateConfig : ScriptableObject
    {
        [SerializeField] private string stateName;
        [SerializeField] private GameObject viewPrefab;
    }
}
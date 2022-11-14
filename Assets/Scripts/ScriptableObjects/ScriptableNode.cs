using System.Collections.Generic;
using ScriptableObjects;
using ScriptableObjects.Utils;
using UnityEngine;

[CreateAssetMenu(fileName = "Node", menuName = "ScriptableObject/ScriptableNode/")]
public class ScriptableNode : ScriptableObject
{
     [SerializeField] private string type;
     [SerializeField] private List<ScriptableNodeState> states = new List<ScriptableNodeState>();
}

using System.Collections.Generic;
using ScriptableObjects;
using ScriptableObjects.Utils;
using UnityEngine;

[CreateAssetMenu(fileName = "Node", menuName = "ScriptableObject/ScriptableNode/Node")]
public class ScriptableNodeConfig : ScriptableObject
{
     [SerializeField] private string type;
     [SerializeField] private List<ScriptableNodeStateConfig> states = new List<ScriptableNodeStateConfig>();
     
     // public 
}

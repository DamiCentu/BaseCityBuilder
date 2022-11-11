using UnityEngine;

namespace Presenters
{
    public class BaseGridNodeView : MonoBehaviour, IView
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, Vector3.one);
        }
    }
}
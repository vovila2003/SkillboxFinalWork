using UnityEngine;

namespace CodeBase.Infrastructure.Markers
{
    public class StartPoint : MonoBehaviour
    {
        private void OnDrawGizmos() {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, 1);
            Gizmos.color = Color.white;
        }
    }
}
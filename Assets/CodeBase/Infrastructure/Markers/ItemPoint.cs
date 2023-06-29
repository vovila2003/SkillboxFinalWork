using CodeBase.Items;
using UnityEngine;

namespace CodeBase.Infrastructure.Markers
{
    public class ItemPoint : MonoBehaviour
    {
        [SerializeField] private ItemType Item;

        public ItemType ItemType => Item;
        
        private void OnDrawGizmos() {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, 0.7f);
            Gizmos.color = Color.white;
        }
    }
}
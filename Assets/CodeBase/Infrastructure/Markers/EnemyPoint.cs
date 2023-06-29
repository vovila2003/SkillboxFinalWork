using CodeBase.Enemies;
using UnityEngine;

namespace CodeBase.Infrastructure.Markers
{
    public class EnemyPoint : MonoBehaviour
    {
        [SerializeField] private EnemyType Enemy;

        public EnemyType EnemyType => Enemy;
        
        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 1);
            Gizmos.color = Color.white;
        }
    }
}
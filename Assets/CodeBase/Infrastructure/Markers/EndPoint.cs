using CodeBase.Common;
using UnityEngine;

namespace CodeBase.Infrastructure.Markers
{
    public class EndPoint : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other) {
            if (!other.transform.CompareTag(Constants.HeroTag)) return;
            Bootstrapper.Instance.Game.Result.WinScreen();
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, 1);
            Gizmos.color = Color.white;
        }
    }
}
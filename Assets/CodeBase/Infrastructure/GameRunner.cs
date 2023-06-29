using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private Bootstrapper BootstrapperPrefab;
        private void Awake() {
            var bootstrapper = FindObjectOfType<Bootstrapper>();
            if (bootstrapper == null)
                Instantiate(BootstrapperPrefab);
        }
    }
}
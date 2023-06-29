using FMODUnity;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.UI
{
    public class InventoryShow : MonoBehaviour
    {
        [Required, SerializeField] private EventReference ShowEvent;
        private Canvas _inventory;

        private void Awake() => 
            _inventory = GetComponent<Canvas>();

        public void ShowInventory() {
            _inventory.enabled = !_inventory.enabled;
            RuntimeManager.PlayOneShot(ShowEvent);
        }
    }
}
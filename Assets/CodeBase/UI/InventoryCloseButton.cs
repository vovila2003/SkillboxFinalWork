using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    [RequireComponent(typeof(Button))]
    public class InventoryCloseButton : MonoBehaviour
    {
        [Required, SerializeField] private InventoryShow Show;
        
        private Button _button;

        private void Awake() {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Show.ShowInventory);
        }
        
        private void OnDestroy() => 
            _button.onClick.RemoveAllListeners();
    }
}
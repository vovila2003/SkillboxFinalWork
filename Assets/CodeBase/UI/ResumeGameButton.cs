using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class ResumeGameButton : MonoBehaviour
    {
        [Required, SerializeField] private PauseMenu PauseMenu;
        private Button _button;
        
        private void Awake() {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(PauseMenu.Hide);
        }
        
        private void OnDestroy() => 
            _button.onClick.RemoveAllListeners();
    }
}
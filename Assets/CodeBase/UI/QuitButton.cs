using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class QuitButton : MonoBehaviour
    {
        private Button _button;

        private void Awake() {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Quit);
        }
        
        private void OnDestroy() => 
            _button.onClick.RemoveAllListeners();

        private void Quit() {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}
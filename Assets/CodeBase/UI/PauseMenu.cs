using UnityEngine;

namespace CodeBase.UI
{
    public class PauseMenu : MonoBehaviour
    {
        private Canvas _canvas;

        private void Awake() => 
            _canvas = GetComponent<Canvas>();

        public void Show() {
            _canvas.enabled = true;
            Time.timeScale = 0;
        }

        public void Hide() {
            _canvas.enabled = false;
            Time.timeScale = 1;
        }
    }
}
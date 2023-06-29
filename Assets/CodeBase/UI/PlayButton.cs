using CodeBase.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    [RequireComponent(typeof(Button))]
    public class PlayButton : MonoBehaviour
    {
        private Button _button;

        private void Awake() {
            _button = GetComponent<Button>();
            
            _button.onClick.AddListener(Bootstrapper.Instance.Game.Play);
        }
        
        private void OnDestroy() => 
            _button.onClick.RemoveAllListeners();
    }
}
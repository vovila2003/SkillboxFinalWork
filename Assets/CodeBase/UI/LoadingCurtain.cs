using CodeBase.Common;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class LoadingCurtain : MonoBehaviour
    {
        [Required, SerializeField] private CanvasGroup CanvasGroup;
        [Required, SerializeField] private Slider Slider; 
        private static LoadingCurtain instance;
        private Canvas _canvas;
        
        public float Value {
            get => Slider.value;
            set => Slider.value = value;
        }

        private void Awake() {
            if (instance != null) {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(this);
        }

        public void Show() {
            CheckCanvas();
            _canvas.enabled = true;
            CanvasGroup.alpha = 1;
            gameObject.SetActive(true);
        }

        public async void Hide() {
            CheckTime();
            CheckCanvas();
            CanvasGroup.DOFade(0, 1f)
                      .SetEase(Ease.InSine)
                      .SetLink(gameObject)
                      .OnComplete(() => _canvas.enabled = false);
            await UniTask.Delay(1000);
            gameObject.SetActive(false);
        }

        private static void CheckTime() {
            if (Mathf.Abs(Time.timeScale - 1) < Constants.Threshold)
                Time.timeScale = 1;
        }

        private void CheckCanvas() {
            if (_canvas != null) return;
            _canvas = GetComponent<Canvas>();
        }
    }
}
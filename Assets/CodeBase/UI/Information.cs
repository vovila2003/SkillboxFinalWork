using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class Information : MonoBehaviour
    {
        [Required, SerializeField] private Text NoPackLabel;
        [Required, SerializeField] private Text PackIsFullLabel;

        public void ShowNoPack() => BlinkLabel(NoPackLabel);
        
        public void ShowPackIsFull() => BlinkLabel(PackIsFullLabel);
        
        private static void BlinkLabel(Text label) {
            label.enabled = true;
            RestoreAlphaColor(label);
            label.DOFade(0, 2)
                 .SetEase(Ease.InBounce)
                 .SetLink(label.gameObject)
                 .OnComplete(() => label.enabled = false);

            void RestoreAlphaColor(Graphic text) {
                var color = text.color;
                color.a = 1;
                text.color = color;
            }
        }
    }
}
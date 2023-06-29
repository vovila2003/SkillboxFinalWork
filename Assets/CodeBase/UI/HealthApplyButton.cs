using CodeBase.Hero;
using CodeBase.UI.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    [RequireComponent(typeof(Button))]
    public class HealthApplyButton : MonoBehaviour, IRegisterHero
    {
        private Button _button;

        private void Awake() => 
            _button = GetComponent<Button>();

        private void OnDestroy() => 
            _button.onClick.RemoveAllListeners();

        public void RegisterHero(GameObject hero) {
            var heroInventory = hero.GetComponent<HeroInventory>();
            if (heroInventory == null) return;
            _button.onClick.AddListener(heroInventory.ApplyHealthBox);
        }
    }
}
using CodeBase.Hero.Abilities;
using CodeBase.UI.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    [RequireComponent(typeof(Button))]
    public class KnifeButton : MonoBehaviour, IRegisterHero
    {
        private Button _button;

        private void Awake() => 
            _button = GetComponent<Button>();

        private void OnDestroy() => 
            _button.onClick.RemoveAllListeners();
        
        public void RegisterHero(GameObject hero) {
            var changeWeaponAbility = hero.GetComponent<ChangeWeaponAbility>();
            if (changeWeaponAbility == null) return;
            _button.onClick.AddListener(changeWeaponAbility.ChangeToKnifeAndExecute);
        }
    }
}
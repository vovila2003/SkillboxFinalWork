using CodeBase.Attack;
using CodeBase.Common;
using CodeBase.UI;
using FMODUnity;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Weapon
{
    public class Wrench : Weapon
    {
        [Required] [SerializeField] private WeaponSettingsSo SettingsSo;
        [Required] [SerializeField] private Transform GunPointTransform;
        
        private KnifeAttackAbility _knifeAttackAbility; 

        private void Start() {
            WeaponSettings = SettingsSo;
            GunPoint = GunPointTransform;
            IsNotEmpty = SettingsSo.IsNotEmpty;
            IsCurrent = false;
        }

        public override void Attack() {
            if(!CheckKnifeAttackAbility()) return;
            _knifeAttackAbility.Execute();
        }

        public override void RegisterModel(UiViewModel _) { }

        public override void RegisterModel(HudViewModel _) { }
        public override void UpdateModels() { }

        public override void PlayAttackSound(bool hit = true) => 
            RuntimeManager.PlayOneShot(hit? Constants.EventWrenchHit : Constants.EventKnifeMiss, transform.position);

        private bool CheckKnifeAttackAbility() {
            if (_knifeAttackAbility != null) return true;
            _knifeAttackAbility = GetComponentInParent<KnifeAttackAbility>();
            return _knifeAttackAbility != null;
        }
    }
}
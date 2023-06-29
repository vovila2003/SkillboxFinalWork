using CodeBase.Attack;
using CodeBase.Common;
using CodeBase.UI;
using FMODUnity;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Weapon
{
    public class Knife : Weapon
    {
        [Required] [SerializeField] private WeaponSettingsSo SettingsSo;
        [Required] [SerializeField] private Transform GunPointTransform;

        private KnifeAttackAbility _knifeAttackAbility;
        private HudViewModel _hudViewModel;
        
        private void Start() {
            WeaponSettings = SettingsSo;
            GunPoint = GunPointTransform;
            IsCurrent = false;
            IsNotEmpty = SettingsSo.IsNotEmpty;
        }

        public override void Attack() {
            if(!CheckKnifeAttackAbility()) return;
            _knifeAttackAbility.Execute();
        }

        public override void RegisterModel(UiViewModel _) { }

        public override void RegisterModel(HudViewModel hudModel) => 
            _hudViewModel = hudModel;

        public override void UpdateModels() {
            _hudViewModel.CurrentBullets = "";
            _hudViewModel.ExtraBullets = "";
        }

        public override void PlayAttackSound(bool hit = true) => 
            RuntimeManager.PlayOneShot(hit? Constants.EventKnifeHit : Constants.EventKnifeMiss, transform.position);

        private bool CheckKnifeAttackAbility() {
            if (_knifeAttackAbility != null) return true;
            _knifeAttackAbility = GetComponentInParent<KnifeAttackAbility>();
            return _knifeAttackAbility != null;
        }
    }
}
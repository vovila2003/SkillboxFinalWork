using CodeBase.Common;
using CodeBase.GraphicEffects;
using CodeBase.UI;
using FMODUnity;
using Sirenix.OdinInspector;
using UnityEngine;
using Settings = CodeBase.Infrastructure.GameData.Settings;

namespace CodeBase.Weapon
{
    public class Beretta : FireArms
    {
        [Required, SerializeField] private WeaponSettingsSo SettingsSo;
        [Required, SerializeField] private Transform GunPointTransform;
        [Required, SerializeField] private Transform FirePointTransform;
        [Required, SerializeField] private EventReference PistolShotEvent;
        [Required, SerializeField] private EventReference PistolReloadEvent;
        [Required, SerializeField] private EventReference PistolEmptyEvent;
        
        private PistolFireEffect.Pool _pistolFireEffectPool;
        
        private void Start() {
            WeaponSettings = SettingsSo;
            GunPoint = GunPointTransform;
            FirePoint = FirePointTransform;
            IsNotEmpty = SettingsSo.IsNotEmpty;
            ReloadTime = Constants.PistolReloadTimeMs;
            UpdateExtraBullets();
            UpdateMaxExtraBullets();
        }

        protected override void AttackImplementation() {
            BulletAttackAbility.Execute();
            CurrentBullets--;
        }

        public override void RegisterModel(UiViewModel inventoryModel) => 
            InventoryModel = inventoryModel;

        public override void RegisterModel(HudViewModel hudModel) => 
            HUDModel = hudModel;

        public override void PlayReloadSound() => 
            RuntimeManager.PlayOneShot(PistolReloadEvent, transform.position);

        public override void PlayEmptySound() => 
            RuntimeManager.PlayOneShot(PistolEmptyEvent, transform.position);

        public override void RegisterPool(PistolFireEffect.Pool pool) => 
            _pistolFireEffectPool = pool;

        public override void RegisterPool(GunFireEffect.Pool pool, Settings settings) { }

        public override void RegisterPool(GunFireEnemyEffect.Pool gunEnemyFireEffectPool) { }
        
        public override void PlayAttackSound(bool hit = true) => 
            RuntimeManager.PlayOneShot(PistolShotEvent, transform.position);

        protected override void ShowFlash() => 
            _pistolFireEffectPool.Spawn(FirePoint);

        protected override void UpdateExtraBullets() {
            if (InventoryModel != null)
                InventoryModel.ExtraPistolBullets = ExtraBulletsValue.ToString();
        }

        protected override void UpdateMaxExtraBullets() {
            if (InventoryModel != null)
                InventoryModel.MaxExtraPistolBullets = MaxExtraBulletsValue.ToString();
        }
    }
}
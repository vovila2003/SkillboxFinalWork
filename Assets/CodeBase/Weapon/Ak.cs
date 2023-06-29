using CodeBase.Common;
using CodeBase.GraphicEffects;
using CodeBase.UI;
using Cysharp.Threading.Tasks;
using FMODUnity;
using Sirenix.OdinInspector;
using UnityEngine;
using Settings = CodeBase.Infrastructure.GameData.Settings;

namespace CodeBase.Weapon
{
    public class Ak : FireArms
    {
        [Required] [SerializeField] private WeaponSettingsSo SettingsSo;
        [Required] [SerializeField] private Transform GunPointTransform;
        [Required] [SerializeField] private Transform FirePointTransform;
        [Required, SerializeField] private EventReference AkShotEvent;
        [Required, SerializeField] private EventReference AkReloadEvent;
        [Required, SerializeField] private EventReference AkEmptyEvent;
        
        private GunFireEffect.Pool _gunFireEffectPool;
        private int _bulletsPerShot;

        private void Start() {
            WeaponSettings = SettingsSo;
            GunPoint = GunPointTransform;
            FirePoint = FirePointTransform;
            IsNotEmpty = SettingsSo.IsNotEmpty;
            ReloadTime = Constants.GunReloadTimeMs;
            UpdateExtraBullets();
            UpdateMaxExtraBullets();
        }

        public override void RegisterModel(UiViewModel inventoryModel) => 
            InventoryModel = inventoryModel;

        public override void RegisterModel(HudViewModel hudModel) => 
            HUDModel = hudModel;

        public override void PlayAttackSound(bool hit = true) => 
            RuntimeManager.PlayOneShot(AkShotEvent, transform.position);

        public override void PlayReloadSound() => 
            RuntimeManager.PlayOneShot(AkReloadEvent, transform.position);

        public override void PlayEmptySound() => 
            RuntimeManager.PlayOneShot(AkEmptyEvent, transform.position);

        public override void RegisterPool(PistolFireEffect.Pool pistolFireEffectPool) { }

        public override void RegisterPool(GunFireEffect.Pool gunFireEffectPool, Settings settings) {
            _gunFireEffectPool = gunFireEffectPool;
            _bulletsPerShot = settings.HeroBulletsPerGunShot;
        }

        public override void RegisterPool(GunFireEnemyEffect.Pool gunEnemyFireEffectPool) { }

        protected override async void AttackImplementation() {
            for (var i = 0; i < _bulletsPerShot; ++i) {
                if (CanShoot) await OneShot();
            }
        }

        protected override void ShowFlash() => 
            _gunFireEffectPool.Spawn(FirePoint);

        protected override void UpdateExtraBullets() {
            if (InventoryModel != null)
                InventoryModel.ExtraGunBullets = ExtraBullets.ToString();
        }

        protected override void UpdateMaxExtraBullets() {
            if (InventoryModel != null)
                InventoryModel.MaxExtraGunBullets = MaxExtraBullets.ToString();
        }

        private async UniTask OneShot() {
            BulletAttackAbility.Execute();
            CurrentBullets--;
            await UniTask.Delay(Constants.GunShotDeltaTimeMs);
        }
    }
}
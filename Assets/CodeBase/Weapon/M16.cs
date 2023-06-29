using CodeBase.Common;
using CodeBase.GraphicEffects;
using CodeBase.UI;
using FMODUnity;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace CodeBase.Weapon
{
    public class M16 : FireArms
    {
        [Required] [SerializeField] private WeaponSettingsSo SettingsSo;
        [Required] [SerializeField] private Transform GunPointTransform;
        [Required] [SerializeField] private Transform FirePointTransform;
        [Required, SerializeField] private EventReference M16ShotEvent;
        [Required, SerializeField] private EventReference M16ReloadEvent;
        [Required, SerializeField] private EventReference M16EmptyEvent;
        
        private GunFireEnemyEffect.Pool _pool;
        
        [Inject]
        private void Construct(GunFireEnemyEffect.Pool pool) => 
            _pool = pool;

        private void Awake() {
            WeaponSettings = SettingsSo;
            GunPoint = GunPointTransform;
            FirePoint = FirePointTransform;
            IsNotEmpty = SettingsSo.IsNotEmpty;
            ReloadTime = Constants.PistolReloadTimeMs;
        }

        public override void RegisterModel(UiViewModel _) { }

        public override void RegisterModel(HudViewModel _) { }

        public override void UpdateModels() { }

        public override void Reload() {
            CurrentBullets = SettingsSo.BulletsInMagazine;
            PlayReloadSound();
        }

        public override void PlayAttackSound(bool hit = true) => 
            RuntimeManager.PlayOneShot(M16ShotEvent, transform.position);

        public override void PlayReloadSound() => 
            RuntimeManager.PlayOneShot(M16ReloadEvent, transform.position);

        public override void PlayEmptySound() => 
            RuntimeManager.PlayOneShot(M16EmptyEvent, transform.position);

        public override void RegisterPool(PistolFireEffect.Pool _) { }

        public override void RegisterPool(GunFireEffect.Pool _, Infrastructure.GameData.Settings settings) { }

        public override void RegisterPool(GunFireEnemyEffect.Pool gunEnemyFireEffectPool) => 
            _pool = gunEnemyFireEffectPool;
        
        protected override void AttackImplementation() {
            BulletAttackAbility.Execute();
            CurrentBullets--;
        }

        protected override void ShowFlash() => 
            _pool.Spawn(FirePoint);

        protected override void UpdateMaxExtraBullets() { }

        protected override void UpdateExtraBullets() { }
    }
}
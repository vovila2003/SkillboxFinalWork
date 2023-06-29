using CodeBase.Attack;
using CodeBase.GraphicEffects;
using CodeBase.Infrastructure.GameData;
using CodeBase.UI;
using CodeBase.Weapon.Interfaces;
using UnityEngine;

namespace CodeBase.Weapon
{
    public abstract class FireArms : Weapon, IFireArms
    {
        
        protected BulletAttackAbility BulletAttackAbility;
        protected HudViewModel HUDModel;
        protected UiViewModel InventoryModel;
        protected int ExtraBulletsValue;
        protected int MaxExtraBulletsValue;
        
        private int _currentBullets;

        public bool CanShoot => CurrentBullets > 0;
        public int ReloadTime { get; protected set; }
        public int ExtraBullets {
            get => ExtraBulletsValue;
            set {
                if (ExtraBulletsValue == value) return;
                ExtraBulletsValue = Mathf.Clamp(value, 0, MaxExtraBullets);
                if (IsCurrent && HUDModel != null)
                    HUDModel.ExtraBullets = ExtraBulletsValue.ToString();
                UpdateExtraBullets();
            }
        }
        public int MaxExtraBullets {
            get => MaxExtraBulletsValue;
            set {
                if (MaxExtraBulletsValue == value) return;
                MaxExtraBulletsValue = value;
                UpdateMaxExtraBullets();
            }
        }
        public bool IsMagazineFull => CurrentBullets == WeaponSettings.BulletsInMagazine;
        
        protected Transform FirePoint { get; set; }
        
        protected int CurrentBullets {
            get => _currentBullets;
            set {
                if (_currentBullets == value) return;
                _currentBullets = Mathf.Clamp(value, 0, WeaponSettings.BulletsInMagazine);
                if (IsCurrent && HUDModel != null)
                    HUDModel.CurrentBullets = _currentBullets.ToString();
            }
        }
        
        public override void Attack() {
            if(!CheckBulletAttackAbility()) return;
            ShowFlash();
            PlayAttackSound();
            AttackImplementation();
        }

        public virtual void Reload() {
            var needed = WeaponSettings.BulletsInMagazine - CurrentBullets;
            if (needed <= ExtraBullets) {
                ExtraBullets -= needed;
                CurrentBullets += needed;
            }
            else {
                CurrentBullets += ExtraBullets;
                ExtraBullets = 0;
            }
        }

        public void SetFullMagazine() => 
            CurrentBullets = WeaponSettings.BulletsInMagazine;
        
        public override void UpdateModels() {
            if (HUDModel == null) return;
            HUDModel.CurrentBullets = CurrentBullets.ToString();
            HUDModel.ExtraBullets = ExtraBullets.ToString();
        }

        public abstract void PlayReloadSound();

        public abstract void PlayEmptySound();

        public abstract void RegisterPool(PistolFireEffect.Pool pistolFireEffectPool);

        public abstract void RegisterPool(GunFireEffect.Pool gunFireEffectPool, Settings settings);

        public abstract void RegisterPool(GunFireEnemyEffect.Pool gunEnemyFireEffectPool);

        protected abstract void ShowFlash();

        protected abstract void AttackImplementation();

        protected abstract void UpdateExtraBullets();

        protected abstract void UpdateMaxExtraBullets();

        private bool CheckBulletAttackAbility() {
            if (BulletAttackAbility != null) return true;
            BulletAttackAbility = GetComponentInParent<BulletAttackAbility>();
            return BulletAttackAbility != null;
        }
    }
}
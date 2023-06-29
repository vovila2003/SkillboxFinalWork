using System;
using System.Collections.Generic;
using CodeBase.Common;
using CodeBase.Common.Interfaces;
using CodeBase.GraphicEffects;
using CodeBase.Hero.Abilities;
using CodeBase.Hero.Interfaces;
using CodeBase.Infrastructure.GameData;
using CodeBase.UI;
using CodeBase.Weapon.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(ChangeWeaponAbility), typeof(HeroAnimator), typeof(HeroCamera))]
    public class HeroWeapon : MonoBehaviour, IWeaponComponent
    {
        [Required] [SerializeField] private GameObject Knife;
        [Required] [SerializeField] private GameObject Pistol;
        [Required] [SerializeField] private GameObject Gun;
        
        private int _tempWeapon;
        private int _pistolBulletsInPack;
        private int _gunBulletsInPack;
        private ChangeWeaponAbility _changeWeaponAbility;
        private ShootAbility _shootAbility;
        private ReloadAbility _reloadAbility;
        private IHeroAnimator _heroAnimator;
        private Settings _settings;
        private UiViewModel _uiViewModel;
        private HeroCamera _heroCamera;
        private MeshRenderer _knifeRenderer;
        private MeshRenderer _pistolRenderer;
        private MeshRenderer _gunRenderer;
        private IWeapon _knife;
        private IFireArms _pistol;
        private IFireArms _gun;
        private PistolFireEffect.Pool _pistolFireEffectPool;
        private GunFireEffect.Pool _gunFireEffectPool;
        private HudViewModel _hudModel;
        private int _maxPistolBullets;
        private int _maxGunBullets;
        private int _startHeroLevel;

        [ShowInInspector] 
        private Dictionary<WeaponType, bool> HeroWeaponArsenal { get; set; }

        public IWeapon CurrentWeapon =>
            CurrentWeaponType switch {
                WeaponType.None => null,
                WeaponType.Knife => _knife,
                WeaponType.HandGun => _pistol,
                WeaponType.MachineGun => _gun,
                _ => throw new ArgumentOutOfRangeException()
            };

        public bool CanBeReloaded {
            get {
                if (CurrentWeapon is IFireArms fireArms) {
                    return fireArms.ExtraBullets > 0;
                }
                return false;
            }
        }

        public bool IsFree => _shootAbility.Ready && _reloadAbility.Ready && _changeWeaponAbility.Ready;  

        private WeaponType CurrentWeaponType { get; set; }

        private bool HasKnife {
            get => HeroWeaponArsenal[WeaponType.Knife];
            set {
                if (HeroWeaponArsenal[WeaponType.Knife] == value) return;
                HeroWeaponArsenal[WeaponType.Knife] = value;
                if (_uiViewModel != null)
                    _uiViewModel.ShowKnife = value;
            }
        }

        private bool HasPistol {
            get => HeroWeaponArsenal[WeaponType.HandGun];
            set {
                if (HeroWeaponArsenal[WeaponType.HandGun] == value) return;
                HeroWeaponArsenal[WeaponType.HandGun] = value;
                if (_uiViewModel != null)
                    _uiViewModel.ShowPistol = value;
            }
        }

        private bool HasGun {
            get => HeroWeaponArsenal[WeaponType.MachineGun];
            set {
                if (HeroWeaponArsenal[WeaponType.MachineGun] == value) return;
                HeroWeaponArsenal[WeaponType.MachineGun] = value;
                if (_uiViewModel != null)
                    _uiViewModel.ShowGun = value;
            }
        }

        [Inject]
        private void Construct(Settings settings,
                               PistolFireEffect.Pool pistolPool,
                               GunFireEffect.Pool gunPool,
                               HeroLevel heroLevel) {
            _settings = settings;
            _pistolFireEffectPool = pistolPool;
            _gunFireEffectPool = gunPool;
            _startHeroLevel = heroLevel.Level;
        }

        private void Awake() {
            _changeWeaponAbility = GetComponent<ChangeWeaponAbility>();
            _shootAbility = GetComponent<ShootAbility>();
            _reloadAbility = GetComponent<ReloadAbility>();
            _heroAnimator = GetComponent<HeroAnimator>();
            _heroCamera = GetComponent<HeroCamera>();
            _knifeRenderer = Knife.GetComponent<MeshRenderer>();
            _pistolRenderer = Pistol.GetComponent<MeshRenderer>();
            _gunRenderer = Gun.GetComponent<MeshRenderer>();
        }

        private void Start() {
            _knife = Knife.GetComponent<IWeapon>();
            if (_knife == null)
                Debug.Log("Knife must inherit IWeapon");
            _pistol = Pistol.GetComponent<IFireArms>();
            if (_pistol == null)
                Debug.Log("Pistol must inherit IFireArms");
            _gun = Gun.GetComponent<IFireArms>();
            if (_gun == null)
                Debug.Log("Gun must inherit IFireArms");
            CurrentWeaponType = WeaponType.None;
            _pistolBulletsInPack = _settings.PistolBulletsInBox;
            _gunBulletsInPack = _settings.GunBulletsInBox;
            RegisterPools();
            _pistol.MaxExtraBullets = _settings.HeroMaxPistolBullets[_startHeroLevel - 1];
            _gun.MaxExtraBullets = _settings.HeroMaxGunBullets[_startHeroLevel - 1];
            RegisterModelsToWeapon();
            _pistol.MaxExtraBullets = _maxPistolBullets;
            _gun.MaxExtraBullets = _maxGunBullets;
        }

        private void RegisterPools() {
            _pistol.RegisterPool(_pistolFireEffectPool);
            _gun.RegisterPool(_gunFireEffectPool, _settings);
        }

        public void RegisterModel(UiViewModel inventoryViewModel) {
            _uiViewModel = inventoryViewModel;
            InitHeroWeapon();
        }

        public void RegisterModel(HudViewModel hudViewModel) => 
            _hudModel = hudViewModel;

        public void ChangeWeapon(ChangeWeaponAbility.ChangeWeaponType changeWeaponType) {
            switch (changeWeaponType) {
                case ChangeWeaponAbility.ChangeWeaponType.Next:
                    NextWeapon((int) CurrentWeaponType);
                    if (_tempWeapon == (int) WeaponType.None && HasWeapon())
                        NextWeapon(_tempWeapon);
                    break;
                case ChangeWeaponAbility.ChangeWeaponType.Knife:
                    if (!TryChangeWeaponTo(WeaponType.Knife)) return;
                    break;
                case ChangeWeaponAbility.ChangeWeaponType.Pistol:
                    if (!TryChangeWeaponTo(WeaponType.HandGun)) return;
                    break;
                case ChangeWeaponAbility.ChangeWeaponType.Gun:
                    if (!TryChangeWeaponTo(WeaponType.MachineGun)) return;
                    break;
                case ChangeWeaponAbility.ChangeWeaponType.HideWeapon:
                    _tempWeapon = (int) WeaponType.None;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(changeWeaponType), changeWeaponType, null);
            }            
            if (_tempWeapon != (int) CurrentWeaponType)
                _heroAnimator.Weapon(_tempWeapon);
            else
                _changeWeaponAbility.Ready = true;
        }

        public void ChangeToKnife() => 
            ChangeWeapon(ChangeWeaponAbility.ChangeWeaponType.Knife);

        public void ChangeToPistol() =>
            ChangeWeapon(ChangeWeaponAbility.ChangeWeaponType.Pistol);

        public void ChangeToGun() =>
            ChangeWeapon(ChangeWeaponAbility.ChangeWeaponType.Gun);

        [Button]
        public void PickUpPistolBullets(BulletsPackType packType = BulletsPackType.Default) {
            _pistol.ExtraBullets += packType switch {
                BulletsPackType.Default => _pistolBulletsInPack,
                BulletsPackType.OneMagazine => _pistol.WeaponSettings.BulletsInMagazine,
                _ => throw new ArgumentOutOfRangeException(nameof(packType), packType, null)
            };
        }

        [Button]
        public void PickUpGunBullets(BulletsPackType packType = BulletsPackType.Default) {
            _gun.ExtraBullets += packType switch {
                BulletsPackType.Default => _gunBulletsInPack,
                BulletsPackType.OneMagazine => _gun.WeaponSettings.BulletsInMagazine,
                _ => throw new ArgumentOutOfRangeException(nameof(packType), packType, null)
            };
        }

        public void SetPistolMaxExtraBullets(int bullets) {
            _maxPistolBullets = bullets;
            if (_pistol != null)
                _pistol.MaxExtraBullets = bullets;
        }

        public void SetGunMaxExtraBullets(int bullets) {
            _maxGunBullets = bullets;
            if (_gun != null)
                _gun.MaxExtraBullets = bullets;
        }

        public bool PickUpKnife() {
            if (HasKnife) return false;
            HasKnife = true;
            return true;
        }

        public void PickUpPistol() {
            if (HasPistol)
                PickUpPistolBullets(BulletsPackType.OneMagazine);
            else {
                HasPistol = true;
                _pistol.SetFullMagazine();
            }
        }

        public void PickUpGun() {
            if (HasGun)
                PickUpGunBullets(BulletsPackType.OneMagazine);
            else {
                HasGun = true;
                _gun.SetFullMagazine();
            }
        }

        public void Reload() {
            if (CurrentWeapon is IFireArms fireArms) 
                fireArms.Reload();
        }

        private void RegisterModelsToWeapon() {
            _knife.RegisterModel(_uiViewModel);
            _pistol.RegisterModel(_uiViewModel);
            _gun.RegisterModel(_uiViewModel);
            _knife.RegisterModel(_hudModel);
            _pistol.RegisterModel(_hudModel);
            _gun.RegisterModel(_hudModel);
        }

        private bool HasWeapon() {
            if (HeroWeaponArsenal == null) return false;
            for (var weapon = WeaponType.Knife; weapon <= WeaponType.MachineGun; ++weapon)
                if (HeroWeaponArsenal[weapon]) return true;
            return false;
        }

        private bool TryChangeWeaponTo(WeaponType weaponType) {
            if (!HeroWeaponArsenal[weaponType]) {
                _changeWeaponAbility.Ready = true;
                return false;
            }
            _tempWeapon = (int) weaponType;
            return true;
        }

        private void NextWeapon(int current) {
            _tempWeapon = (current + 1) % 4;
            while (_tempWeapon != current) {
                if (HeroWeaponArsenal[(WeaponType) _tempWeapon]) break;
                _tempWeapon = (_tempWeapon + 1) % 4;
            }
        }

        private void InitHeroWeapon() {
            HeroWeaponArsenal = new Dictionary<WeaponType, bool>(4) {
                [WeaponType.None] = true,
                [WeaponType.Knife] = false,
                [WeaponType.HandGun] = false,
                [WeaponType.MachineGun] = false,
            };
        }

        private void WeaponChanged() {
            if (CurrentWeapon != null)
                CurrentWeapon.IsCurrent = false;
            CurrentWeaponType = (WeaponType) _tempWeapon;
            if (CurrentWeapon != null)
                CurrentWeapon.IsCurrent = true;
            _changeWeaponAbility.Ready = true;
            _heroCamera.ChangeView(NoWeaponInArms());
            CurrentWeapon?.UpdateModels();
        }

        private bool NoWeaponInArms() => 
            CurrentWeaponType == WeaponType.None;

        private void HideKnife() =>
            _knifeRenderer.enabled = false;

        private void ShowKnife() => 
            _knifeRenderer.enabled = true;

        private void HidePistol() =>
            _pistolRenderer.enabled = false;

        private void ShowPistol() =>
            _pistolRenderer.enabled = true;

        private void HideGun() =>
            _gunRenderer.enabled = false;

        private void ShowGun() =>
            _gunRenderer.enabled = true;
    }
}

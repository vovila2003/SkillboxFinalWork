using CodeBase.Common;
using CodeBase.Common.Interfaces;
using CodeBase.Hero.Interfaces;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.GameData;
using CodeBase.UI;
using CodeBase.Weapon.Interfaces;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(HeroAnimator), typeof(HeroArmor), typeof(HeroSound))]
    public class HeroHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private bool GodMode;
        private float _health;
        private Settings _settings;
        private float _maxHealth;
        private int _healthValueIsBox;
        private IHeroAnimator _heroAnimator;
        private HudViewModel _hudViewModel;
        private UiViewModel _uiViewModel;
        private HeroArmor _heroArmor;
        private HeroLevel _heroLevel;
        private HeroSound _heroSound;

        [ShowInInspector] 
        public bool IsDead { get; private set; }

        public bool IsFull => Mathf.Abs(Current - MaxHealth) < Constants.Threshold;
        
        [ShowInInspector]
        private float Current {
            get => _health;
            set {
                if (Mathf.Abs(_health - value) < Constants.Threshold) return;
                _health = value;
                if (_health > MaxHealth) 
                    _health = MaxHealth;
                if (_hudViewModel != null) {
                    var health = (int) Mathf.Ceil(_health);
                    _hudViewModel.Health = health > 0 ? health.ToString() : "0";
                }
                if (!(_health <= 0) || IsDead) return;
                HeroDie();
            }
        }

        [ShowInInspector]
        public float MaxHealth {
            get => _maxHealth;
            set {
                if (Mathf.Abs(_maxHealth - value) < Constants.Threshold) return;
                _maxHealth = value;
                if (_uiViewModel != null)
                    _uiViewModel.MaxHealth = ((int) _maxHealth).ToString();
            }
        }

        [Inject]
        private void Construct(Settings settings, HeroLevel heroLevel) {
            _settings = settings;
            _heroLevel = heroLevel;
        }

        private void Awake() {
            _heroAnimator = GetComponent<HeroAnimator>();
            _heroArmor = GetComponent<HeroArmor>();
            _heroSound = GetComponent<HeroSound>();
        }

        private void Start() {
            _healthValueIsBox = _settings.HealthValueInBox;
            IsDead = false;
        }
        
        public void RegisterModel(UiViewModel inventoryViewModel) {
            _uiViewModel = inventoryViewModel;
            MaxHealth = _settings.HeroMaxHealth[_heroLevel.Level - 1];
        }

        public void RegisterModel(HudViewModel hudViewModel) {
            _hudViewModel = hudViewModel;
            Current = _settings.HeroMaxHealth[_heroLevel.Level - 1];
        }

        [Button]
        public void Damage(IWeapon weapon) {
            if (GodMode) return;
            var weaponSettings = weapon.WeaponSettings;
            var armorDamage = weaponSettings.Damage * (1 - weaponSettings.ArmorPenetration);
            if (_heroArmor.Current >= armorDamage) {
                _heroArmor.Current -= armorDamage;
                Current -= weaponSettings.Damage * weaponSettings.ArmorPenetration;
            }
            else {
                Current -= weaponSettings.Damage - _heroArmor.Current;
                _heroArmor.Current = 0;
            }
            _heroSound.PlayDamage();
        }

        [Button] 
        public void Heal(float value = 50) => 
            Current += value;

        public void ApplyHealthBox() {
            Current += _healthValueIsBox;
            _heroSound.PlayApplyHealth();
        }

        private async void HeroDie() {
            IsDead = true;
            _heroAnimator.Die();
            _heroSound.PlayDeath();
            await UniTask.Delay(Constants.HeroDieDelayMs);
            _heroLevel.Level = 1;
            Bootstrapper.Instance.Game.Result.LoseScreen();
        }
    }
}
using System;
using CodeBase.Common;
using CodeBase.Infrastructure.GameData;
using CodeBase.UI;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero
{
    public class HeroModel : MonoBehaviour
    {
        private InitialViewModel _viewModel;
        private Settings _settings;
        private HeroEquipment _heroEquipment;
        private HeroLevel _heroLevel;
        private int _level;
        private float _health;
        private float _armor;
        private float _maxHealth;
        private float _maxArmor;
        private int _maxItemCount;
        private int _maxPistolBullets;
        private int _maxGunBullets;

        private int Level {
            get => _level;
            set {
                if (_level == value) return;
                _level = value;
                if (_viewModel != null) {
                    _viewModel.Level = _level.ToString();
                }
            }
        }
        
        private float Health {
            set {
                if (Math.Abs(_health - value) < Constants.Threshold) return;
                _health = value;
                if (_viewModel == null) return;
                var health = (int) Mathf.Ceil(_health);
                _viewModel.Health = health.ToString();
            }
        }
        
        private float Armor {
            set {
                if (Math.Abs(_armor - value) < Constants.Threshold) return;
                _armor = value;
                if (_viewModel == null) return;
                var armor = (int) Mathf.Ceil(_armor);
                _viewModel.Armor = armor.ToString();
            }
        }
        
        private float MaxHealth {
            set {
                if (Math.Abs(_maxHealth - value) < Constants.Threshold) return;
                _maxHealth = value;
                if (_viewModel == null) return;
                var health = (int) Mathf.Ceil(_maxHealth);
                _viewModel.MaxHealth = health.ToString();
            }
        }
        
        private float MaxArmor {
            set {
                if (Math.Abs(_maxArmor - value) < Constants.Threshold) return;
                _maxArmor = value;
                if (_viewModel == null) return;
                var armor = (int) Mathf.Ceil(_maxArmor);
                _viewModel.MaxArmor = armor.ToString();
            }
        }
        
        private int MaxItemCount {
            set {
                if (_maxItemCount == value) return;
                _maxItemCount = value;
                if (_viewModel == null) return;
                _viewModel.MaxItemCount = _maxItemCount.ToString();
            }
        }
        
        private int MaxPistolBullets {
            set {
                if (_maxPistolBullets == value) return;
                _maxPistolBullets = value;
                if (_viewModel == null) return;
                _viewModel.MaxPistolBullets = _maxPistolBullets.ToString();
            }
        }
        
        private int MaxGunBullets {
            set {
                if (_maxGunBullets == value) return;
                _maxGunBullets = value;
                if (_viewModel == null) return;
                _viewModel.MaxGunBullets = _maxGunBullets.ToString();
            }
        }

        [Inject]
        private void Construct(Settings settings, HeroLevel heroLevel) {
            _settings = settings;
            _heroLevel = heroLevel;
        }

        private void Start() =>
            transform.DORotate(new Vector3(0, 360, 0), _settings.HeroMenuRotatePeriod, RotateMode.FastBeyond360)
                     .SetEase(Ease.Linear)
                     .SetLink(gameObject)
                     .SetRelative()
                     .SetLoops(-1, LoopType.Restart);

        public void Register(InitialViewModel model) {
            _heroEquipment = GetComponent<HeroEquipment>();
            _viewModel = model;
            SetupTableValue();
            SetupHeroModel();
        }

        private void SetupTableValue() {
            Level = _heroLevel.Level;
            if (Level < 1 || Level > 6) return;
            Health = _settings.HeroMaxHealth[Level - 1];
            Armor = Level == 1? 0 : _settings.HeroMaxArmor[Level - 1] / 2;
            MaxHealth = _settings.HeroMaxHealth[Level - 1];
            MaxArmor = _settings.HeroMaxArmor[Level - 1];
            MaxItemCount = _settings.HeroMaxInventoryItems[Level - 1];
            MaxPistolBullets = _settings.HeroMaxPistolBullets[Level - 1];
            MaxGunBullets = _settings.HeroMaxGunBullets[Level - 1];
        }

        private void SetupHeroModel() => 
            _heroEquipment.SetEquipment(Level);
    }
}
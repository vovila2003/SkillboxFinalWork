using CodeBase.Common;
using CodeBase.Infrastructure.GameData;
using CodeBase.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(HeroSound))]
    public class HeroArmor : MonoBehaviour
    {
        private float _armor = -1;
        [ShowInInspector] private float _maxArmor;
        private int _armorValueInBox;
        private Settings _settings;
        private HudViewModel _hudViewModel;
        private UiViewModel _uiViewModel;
        private HeroLevel _heroLevel;
        private HeroSound _heroSound;
        
        public bool IsFull => Mathf.Abs(Current - MaxArmor) < Constants.Threshold;

        [ShowInInspector]
        public float Current {
            get => _armor;
            set {
                if (Mathf.Abs(_armor - value) < Constants.Threshold) return;
                _armor = value;
                if (_armor > MaxArmor)
                    _armor = MaxArmor;
                if (_armor < 0) _armor = 0;
                if (_hudViewModel == null) return;
                var armor = (int) Mathf.Ceil(_armor);
                _hudViewModel.Armor = armor > 0 ? armor.ToString() : "0";
            }
        }

        [ShowInInspector]
        public float MaxArmor {
            get => _maxArmor;
            set {
                if (Mathf.Abs(_maxArmor - value) < Constants.Threshold) return;
                _maxArmor = value;
                if (_uiViewModel != null)
                    _uiViewModel.MaxArmor = ((int)_maxArmor).ToString();
            }
        }
        
        [Inject]
        private void Construct(Settings settings, HeroLevel heroLevel) {
            _settings = settings;
            _heroLevel = heroLevel;
        }

        private void Awake() => 
            _heroSound = GetComponent<HeroSound>();

        private void Start() => 
            _armorValueInBox = _settings.ArmorValueInBox;

        public void Register(UiViewModel inventoryViewModel) {
            _uiViewModel = inventoryViewModel;
            MaxArmor = _settings.HeroMaxArmor[_heroLevel.Level - 1];
        }

        public void Register(HudViewModel hudViewModel) {
            _hudViewModel = hudViewModel;
            var level = _heroLevel.Level;
            Current = level == 1? 0 : _settings.HeroMaxArmor[level - 1] / 2; 
        }

        [Button]
        public void Increase(float value = 50) => 
            Current += value;

        public void ApplyArmorBox() {
            Current += _armorValueInBox;
            _heroSound.PlayApplyArmor();
        }
    }
}
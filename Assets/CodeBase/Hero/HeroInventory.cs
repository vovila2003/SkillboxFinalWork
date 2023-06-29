using System.Collections.Generic;
using System.Linq;
using CodeBase.Hero.Abilities;
using CodeBase.Infrastructure.GameData;
using CodeBase.Items;
using CodeBase.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(InteractAbility), typeof(HeroWeapon), typeof(HeroEquipment))]
    [RequireComponent(typeof(PickUpItemAbility), typeof(HeroHealth), typeof(HeroArmor))]
    [RequireComponent(typeof(HeroExperienceLevel))]
    public class HeroInventory : MonoBehaviour
    {
        private readonly Dictionary<ItemType, int> _items = new Dictionary<ItemType, int>(2) {
            [ItemType.HealthBox] = 0,
            [ItemType.ArmorBox] = 0,
        };

        private InteractAbility _interactAbility;
        private PickUpItemAbility _pickUpItemAbility;
        private HeroEquipment _heroEquipment;
        private HeroHealth _heroHealth;
        private HeroArmor _heroArmor;
        private HeroExperienceLevel _heroExperienceLevel;
        private int _maxItemCount;
        private Settings _settings;
        private UiViewModel _uiViewModel;
        private bool _hasPack;
        private Information _information;

        [ShowInInspector]
        public bool HasPack {
            get => _hasPack;
            private set {
                if (_hasPack == value) return;
                _hasPack = value;
                _heroEquipment.SetPack(value);
            }
        }

        [ShowInInspector] 
        public bool PackIsFull => SumAllItems() >= MaxItemCount;  

        [ShowInInspector]
        public int MaxItemCount {
            get => _maxItemCount;
            set {
                if (_maxItemCount == value) return;
                if (value < 0 || value > _settings.HeroMaxInventoryItems[_settings.HeroMaxInventoryItems.Count - 1]) return;
                _maxItemCount = value;
                if (_uiViewModel != null)
                    _uiViewModel.MaxItemCount = _maxItemCount.ToString();
            }
        }
        
        [ShowInInspector]
        private int HealthCount {
            get => _items[ItemType.HealthBox];
            set {
                const ItemType type = ItemType.HealthBox;
                if (_items[type] == value) return;
                if (value + SumAnotherItems(type) > MaxItemCount) {
                    if (_information != null)
                        _information.ShowPackIsFull();
                    return;
                }
                _items[type] = value;
                if (_uiViewModel == null) return;
                _uiViewModel.HealthCount = value.ToString();
                _uiViewModel.ShowHealth = _items[type] > 0;
            }
        }

        [ShowInInspector]
        private int ArmorCount {
            get => _items[ItemType.ArmorBox];
            set {
                const ItemType type = ItemType.ArmorBox;
                if (_items[type] == value) return;
                if (value + SumAnotherItems(type) > MaxItemCount) {
                    if (_information != null)
                        _information.ShowPackIsFull();
                    return;
                }
                _items[type] = value;
                if (_uiViewModel == null) return;
                _uiViewModel.ArmorCount = value.ToString();
                _uiViewModel.ShowArmor = _items[type] > 0;
            }
        }

        [Inject]
        private void Construct(Settings settings) {
            _settings = settings;
        }

        private void Awake() {
            _interactAbility = GetComponent<InteractAbility>();
            _pickUpItemAbility = GetComponent<PickUpItemAbility>();
            _heroEquipment = GetComponent<HeroEquipment>();
            _heroHealth = GetComponent<HeroHealth>();
            _heroArmor = GetComponent<HeroArmor>();
            _heroExperienceLevel = GetComponent<HeroExperienceLevel>();
            _hasPack = false;
        }

        public void Register(UiViewModel inventoryViewModel) {
            _uiViewModel = inventoryViewModel;
            MaxItemCount = _settings.HeroMaxInventoryItems[0];
            TouchHealthCount();
            TouchArmorCount();
        }

        public void Register(Information info) => 
            _information = info;

        [Button]
        public void PickUpPack() => 
            HasPack = true;

        [Button]
        public void AddHealthBox() {
            HealthCount++;
        }

        [Button]
        public void AddArmorBox() => 
            ArmorCount++;

        public void ApplyHealthBox() {
            if (HealthCount <= 0 || _heroHealth.IsFull) return;
            HealthCount--;
            _heroHealth.ApplyHealthBox();
            _heroExperienceLevel.ApplyItemBonus();
        }

        public void ApplyArmorBox() {
            if (ArmorCount <= 0 || _heroArmor.IsFull) return;
            ArmorCount--;
            _heroArmor.ApplyArmorBox();
            _heroExperienceLevel.ApplyItemBonus();
        }

        private void TouchArmorCount() {
            var tmp = ArmorCount;
            ArmorCount = tmp == 0 ? 1 : 0;
            ArmorCount = tmp;
        }

        private void TouchHealthCount() {
            var tmp = HealthCount;
            HealthCount = tmp == 0 ? 1 : 0;
            HealthCount = tmp;
        }

        private void InteractCompleted() {
            _interactAbility.Ready = true;
            _pickUpItemAbility.Execute();
        }

        private int SumAnotherItems(ItemType except) =>
            _items.Where(item => item.Key != except)
                  .Sum(item => item.Value);

        private int SumAllItems() => 
            _items.Sum(item => item.Value);
    }
}
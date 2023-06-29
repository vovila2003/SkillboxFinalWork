using CodeBase.Hero.Interfaces;
using CodeBase.Infrastructure.GameData;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero.LevelUpActions
{
    [RequireComponent(typeof(HeroInventory))]
    public class LevelUpInventory : MonoBehaviour, ILevelUp
    {
        private Settings _settings;
        private HeroInventory _heroInventory;

        [Inject]
        private void Construct(Settings settings) => 
            _settings = settings;

        private void Awake() => 
            _heroInventory = GetComponent<HeroInventory>();

        public void LevelUp(int currentLevel) {
            if (currentLevel >= 1 && currentLevel <= 6)
                _heroInventory.MaxItemCount = _settings.HeroMaxInventoryItems[currentLevel - 1];
        }
    }
}
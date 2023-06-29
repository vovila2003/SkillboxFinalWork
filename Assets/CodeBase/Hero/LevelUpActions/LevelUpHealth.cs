using CodeBase.Hero.Interfaces;
using CodeBase.Infrastructure.GameData;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero.LevelUpActions
{
    [RequireComponent(typeof(HeroHealth))]
    public class LevelUpHealth : MonoBehaviour, ILevelUp
    {
        private Settings _settings;
        private HeroHealth _heroHealth;

        [Inject]
        private void Construct(Settings settings) => 
            _settings = settings;

        private void Awake() => 
            _heroHealth = GetComponent<HeroHealth>();

        public void LevelUp(int currentLevel) {
            if (currentLevel >= 1 && currentLevel <= 6)
                _heroHealth.MaxHealth = _settings.HeroMaxHealth[currentLevel - 1];
        }
    }
}
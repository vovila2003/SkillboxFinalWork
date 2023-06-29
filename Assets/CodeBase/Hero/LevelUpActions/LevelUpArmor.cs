using CodeBase.Hero.Interfaces;
using CodeBase.Infrastructure.GameData;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero.LevelUpActions
{
    [RequireComponent(typeof(HeroArmor))]
    public class LevelUpArmor : MonoBehaviour, ILevelUp
    {
        private HeroArmor _heroArmor;
        private Settings _settings;

        [Inject]
        private void Construct(Settings settings) => 
            _settings = settings;

        private void Awake() => 
            _heroArmor = GetComponent<HeroArmor>();

        public void LevelUp(int currentLevel) {
            if (currentLevel >= 1 && currentLevel <= 6)
                _heroArmor.MaxArmor = _settings.HeroMaxArmor[currentLevel - 1];
        }
    }
}
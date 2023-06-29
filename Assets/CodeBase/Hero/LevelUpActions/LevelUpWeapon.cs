using CodeBase.Hero.Interfaces;
using CodeBase.Infrastructure.GameData;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero.LevelUpActions
{
    [RequireComponent(typeof(HeroWeapon))]
    public class LevelUpWeapon : MonoBehaviour, ILevelUp
    {
        private Settings _settings;
        private HeroWeapon _heroWeapon;

        [Inject]
        private void Construct(Settings settings) => 
            _settings = settings;

        private void Awake() => 
            _heroWeapon = GetComponent<HeroWeapon>();

        public void LevelUp(int currentLevel) {
            if (currentLevel < 1 || currentLevel > 6) return;
            _heroWeapon.SetPistolMaxExtraBullets(_settings.HeroMaxPistolBullets[currentLevel - 1]);
            _heroWeapon.SetGunMaxExtraBullets(_settings.HeroMaxGunBullets[currentLevel - 1]);
        }
    }
}
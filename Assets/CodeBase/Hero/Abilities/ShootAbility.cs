using CodeBase.Common.Interfaces;
using CodeBase.Hero.Interfaces;
using CodeBase.Infrastructure.GameData;
using CodeBase.Weapon.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero.Abilities
{
    [RequireComponent(typeof(ReloadAbility),typeof(HeroExperienceLevel), typeof(HeroAnimator))]
    public class ShootAbility : MonoBehaviour, IAbility
    {
        private Settings _settings;
        private IAbility _reloadAbility;
        private HeroAnimator _heroAnimator;
        private HeroExperienceLevel _heroExperienceLevel;
        private float _shootTime = float.MinValue;
        private float _shootDelay;
        private IWeaponComponent _weaponComponent;

        public bool Ready { get; private set; } = true;

        [Inject]
        private void Construct(Settings settings) => 
            _settings = settings;

        private void Awake() {
            _reloadAbility = GetComponent<ReloadAbility>();
            _heroExperienceLevel = GetComponent<HeroExperienceLevel>();
            _heroAnimator = GetComponent<HeroAnimator>();
            _weaponComponent = GetComponent<IWeaponComponent>();
        }

        private void Start() => 
            _shootDelay = _settings.HeroShootDelay;

        public async void Execute() {
            if (!_weaponComponent.IsFree || !TimeToShoot()) return;
            var currentWeapon = _weaponComponent.CurrentWeapon;
            if (!currentWeapon!.IsNotEmpty) return;
            if (currentWeapon is IFireArms {CanShoot: false}) {
                _reloadAbility.Execute();
                return;
            }
            Ready = false;
            _heroAnimator.Attack();
            currentWeapon.Attack();
            _heroExperienceLevel.ShotBonus();
            await UniTask.Delay((int)(_shootDelay * 1000));
            Ready = true;
        }

        private bool TimeToShoot() {
            var time = Time.time;
            if (time < _shootTime + _shootDelay) return false;
            _shootTime = time;
            return true;
        }
    }
}
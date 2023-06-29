using CodeBase.Common;
using CodeBase.Common.Interfaces;
using CodeBase.Hero.Interfaces;
using CodeBase.Weapon.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Hero.Abilities
{
    [RequireComponent(typeof(HeroAnimator))]
    public class ReloadAbility : MonoBehaviour, IAbility
    {
        private HeroAnimator _heroAnimator;
        private float _reloadTime = float.MinValue;
        private float _reloadDelay;
        private IWeaponComponent _weaponComponent;

        public bool Ready { get; private set; } = true;

        private void Awake() {
            _heroAnimator = GetComponent<HeroAnimator>();
        }

        private void Start() {
            var hasWeapon = GetComponent<IHasWeapon>();
            if (hasWeapon != null )
                _weaponComponent = hasWeapon.WeaponComponent;
            else {
                Debug.Log("Object hasn't IHasWeapon");
            }
            _reloadDelay = Constants.ReloadDelay;
        }

        public async void Execute() {
            if (!_weaponComponent.IsFree || !Ready || !TimeToReload()) return;
            Ready = false;
            await Reload();
            Ready = true;
        }

        private bool TimeToReload() {
            var time = Time.time;
            if (time < _reloadTime + _reloadDelay) return false;
            _reloadTime = time;
            return true;
        }
        
        private async UniTask Reload() {
            if (_weaponComponent.CurrentWeapon is IFireArms weapon) {
                if (_weaponComponent.CanBeReloaded && !weapon.IsMagazineFull) {
                    _heroAnimator.Reload();
                    weapon.PlayReloadSound();
                    await UniTask.Delay(weapon.ReloadTime);
                    _weaponComponent.Reload();
                }
                else {
                    weapon.PlayEmptySound();
                }
            }
        }
    }
}
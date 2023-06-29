using CodeBase.Common.Interfaces;
using CodeBase.Weapon.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Enemies
{
    public class EnemyWeapon : MonoBehaviour, IWeaponComponent
    {
        [Required] [SerializeField] private GameObject WeaponGameObject;

        private int _currentBullets;
        private IWeapon _weapon;
        private IFireArms _fireArms;

        public IWeapon CurrentWeapon => _weapon;

        public bool CanBeReloaded => true;
        public bool IsFree { get; } = true;

        private void Start() {
            _weapon = WeaponGameObject.GetComponent<IWeapon>();
            if (_weapon == null)
                Debug.Log("WeaponGameObject must inherit IWeapon");
            _fireArms = _weapon as IFireArms;
            _fireArms?.SetFullMagazine();
        }

        public void Reload() => 
            _fireArms?.Reload();
    }
}
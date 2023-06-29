using CodeBase.UI;
using CodeBase.Weapon.Interfaces;
using UnityEngine;

namespace CodeBase.Weapon
{
    public abstract class Weapon : MonoBehaviour, IWeapon
    {
        public WeaponSettingsSo WeaponSettings { get; protected set; }
        public Transform GunPoint { get; protected set; }
        public bool IsNotEmpty { get; protected set; }
        public bool IsCurrent { get; set; } = false;

        public abstract void PlayAttackSound(bool hit = true);

        public abstract void Attack();

        public abstract void RegisterModel(UiViewModel inventoryModel);

        public abstract void RegisterModel(HudViewModel hudModel);

        public abstract void UpdateModels();
    }
}
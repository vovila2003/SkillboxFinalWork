using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Weapon.Interfaces
{
    public interface IWeapon
    {
        WeaponSettingsSo WeaponSettings { get; }
        Transform GunPoint { get; }
        bool IsNotEmpty { get; }
        void PlayAttackSound(bool hit);
        void Attack();
        void RegisterModel(UiViewModel inventoryModel);
        void RegisterModel(HudViewModel hudModel);
        void UpdateModels();
        bool IsCurrent { get; set; }
    }
}
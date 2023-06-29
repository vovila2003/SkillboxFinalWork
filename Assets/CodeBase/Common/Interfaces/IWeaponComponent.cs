using CodeBase.Weapon.Interfaces;

namespace CodeBase.Common.Interfaces
{
    public interface IWeaponComponent
    {
        IWeapon CurrentWeapon { get; }
        bool CanBeReloaded { get; }
        bool IsFree { get; }
        void Reload();
    }
}
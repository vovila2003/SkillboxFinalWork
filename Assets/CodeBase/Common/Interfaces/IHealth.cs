using CodeBase.Weapon;
using CodeBase.Weapon.Interfaces;

namespace CodeBase.Common.Interfaces
{
    public interface IHealth
    {
        bool IsDead { get; }
        void Damage(IWeapon weapon);
    }
}
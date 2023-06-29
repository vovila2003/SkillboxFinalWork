using CodeBase.Hero.Abilities;

namespace CodeBase.Hero.Interfaces
{
    public interface IAbility
    {
        void Execute();
    }
    
    public interface IPayloadAbility : IAbility
    {
        void ChangeTo(ChangeWeaponAbility.ChangeWeaponType changeWeaponType);
    }
}
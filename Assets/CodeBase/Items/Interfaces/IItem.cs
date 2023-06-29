using CodeBase.Hero;

namespace CodeBase.Items.Interfaces
{
    public interface IItem
    {
        bool CanBePickedUpWithoutBackpack { get; }
        bool CanBePickedUpWhenBackpackIsFull { get; }
        void PickUpItem(HeroInventory heroInventory);
        void PickUpItem(HeroWeapon heroWeapon);
    }
}
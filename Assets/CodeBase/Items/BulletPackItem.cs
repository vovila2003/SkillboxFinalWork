using CodeBase.Hero;
using CodeBase.Items.Interfaces;
using UnityEngine;

namespace CodeBase.Items
{
    public class BulletPackItem : MonoBehaviour, IItem
    {
        [SerializeField] private bool PickUpWithoutBackpack;
        [SerializeField] private bool PickUpWhenBackpackIsFull;
        
        public bool CanBePickedUpWithoutBackpack => PickUpWithoutBackpack;
        public bool CanBePickedUpWhenBackpackIsFull => PickUpWhenBackpackIsFull;

        public void PickUpItem(HeroInventory heroInventory) { }

        public void PickUpItem(HeroWeapon heroWeapon) {
            heroWeapon.PickUpPistolBullets();
            heroWeapon.PickUpGunBullets();
        }
    }
}
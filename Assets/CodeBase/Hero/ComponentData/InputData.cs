using Unity.Entities;
using Unity.Mathematics;

namespace CodeBase.Hero.ComponentData
{
    public struct InputData : IComponentData
    {
        public float2 Move;
        public float Shoot;
        public float ChangeWeapon;
        public float Reload;
        public float Inventory;
        public float Interact;
        public float Exit;
        public float Armor;
        public float Heal;
        public float Knife;
        public float Pistol;
        public float Gun;
        public float NoWeapon;
    }
}
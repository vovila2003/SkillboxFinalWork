using CodeBase.Items;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories.Interfaces
{
    public interface IItemFactory
    {
        void CreateHealthBox(Vector3 at);
        void CreateArmorBox(Vector3 at);
        void CreateBackPack(Vector3 at);
        void CreateBulletsPack(Vector3 at);
        void CreateKnife(Vector3 at);
        void CreatePistol(Vector3 at);
        void CreateGun(Vector3 at);
        void Create(Vector3 at, ItemType type);
    }
}
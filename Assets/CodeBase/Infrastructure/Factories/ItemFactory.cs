using System;
using CodeBase.Infrastructure.Factories.Interfaces;
using CodeBase.Infrastructure.GameData.Interfaces;
using CodeBase.Items;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure.Factories
{
    public class ItemFactory : IItemFactory
    {
        private readonly DiContainer _diContainer;
        private readonly IPrefabs _prefabs;

        public ItemFactory(DiContainer diContainer, IPrefabs prefabs) {
            _diContainer = diContainer;
            _prefabs = prefabs;
        }

        public void CreateHealthBox(Vector3 at) => 
            Create(at, ItemType.HealthBox);
        
        public void CreateArmorBox(Vector3 at) => 
            Create(at, ItemType.ArmorBox);
        
        public void CreateBackPack(Vector3 at) => 
            Create(at, ItemType.BackPack);
        
        public void CreateBulletsPack(Vector3 at) => 
            Create(at, ItemType.BulletsPack);
        
        public void CreateKnife(Vector3 at) => 
            Create(at, ItemType.Knife);
        
        public void CreatePistol(Vector3 at) => 
            Create(at, ItemType.Pistol);
        
        public void CreateGun(Vector3 at) => 
            Create(at, ItemType.Gun);

        public void Create(Vector3 at, ItemType type) {
            var prefab = ItemPrefabByType(type);
            var item = Object.Instantiate(prefab, at, prefab.transform.rotation);
            _diContainer.Inject(item);
        }

        private GameObject ItemPrefabByType(ItemType type) =>
            type switch {
                ItemType.HealthBox => _prefabs.HealthBoxPrefab,
                ItemType.ArmorBox => _prefabs.ArmorBoxPrefab,
                ItemType.BackPack => _prefabs.BackPackPrefab,
                ItemType.BulletsPack => _prefabs.BulletsBoxPrefab,
                ItemType.Knife => _prefabs.KnifePrefab,
                ItemType.Pistol => _prefabs.PistolPrefab,
                ItemType.Gun => _prefabs.MachineGunPrefab,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
    }
}
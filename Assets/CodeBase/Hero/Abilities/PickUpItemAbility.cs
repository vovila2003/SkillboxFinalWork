using System.Collections.Generic;
using CodeBase.Common;
using CodeBase.Common.ComponentData;
using CodeBase.Hero.Interfaces;
using CodeBase.Items;
using CodeBase.Items.ComponentData;
using CodeBase.Items.Interfaces;
using CodeBase.UI;
using Sirenix.OdinInspector;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace CodeBase.Hero.Abilities
{
    [RequireComponent(typeof(HeroWeapon), typeof(HeroExperienceLevel), typeof(HeroSound))]
    public class PickUpItemAbility : MonoBehaviour, IAbility, IConvertGameObjectToEntity
    {
        [Required, SerializeField] private Collider Collider;

        private HeroInventory _heroInventory;
        private HeroWeapon _heroWeapon;
        private HeroExperienceLevel _heroExperienceLevel;
        private Information _information;
        private HeroSound _heroSound;

        public List<Collider> Collisions { get; } = new List<Collider>();

        private void Awake() {
            _heroInventory = GetComponent<HeroInventory>();
            _heroWeapon = GetComponent<HeroWeapon>();
            _heroExperienceLevel = GetComponent<HeroExperienceLevel>();
            _heroSound = GetComponent<HeroSound>();
        }

        public void Execute() {
            var showPackFirst = false;
            foreach (var itemCollider in Collisions) {
                var itemEntity = itemCollider.GetComponent<ItemEntity>();
                var item = itemCollider.GetComponent<IItem>();
                if (itemEntity == null || item == null) continue;
                if (!CanPickUp(item)) {
                    showPackFirst = true;                            
                    continue;
                }
                showPackFirst = false;
                if (PickUpItem(item)) {
                    _heroSound.PlayTakeItem();
                    DestroyItem(itemEntity);
                    TakeExperience();
                }
                else if (_heroInventory.PackIsFull && _information != null)
                    _information.ShowPackIsFull();
            }

            if (showPackFirst && _information != null) {
                _information.ShowNoPack();
            }
        }

        public void Register(Information info) =>
            _information = info;
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
            float3 position = gameObject.transform.position;
            switch(Collider) {
                case SphereCollider sphere:
                    sphere.ToWorldSpaceSphere(out var sphereCenter, out var sphereRadius);
                    dstManager.AddComponentData(entity, new ItemColliderData{
                        ColliderType = ColliderType.Sphere,
                        SphereCenter = sphereCenter - position,
                        SphereRadius = sphereRadius,
                    });
                    break;
                case CapsuleCollider capsule:
                    capsule.ToWorldSpaceCapsule(out var capsuleStart, out var capsuleEnd, 
                        out var capsuleRadius);
                    dstManager.AddComponentData(entity, new ItemColliderData{
                        ColliderType = ColliderType.Capsule,
                        CapsuleStart = capsuleStart - position,
                        CapsuleEnd = capsuleEnd - position,
                        CapsuleRadius = capsuleRadius,
                    });
                    break;
                case BoxCollider box:
                    box.ToWorldSpaceBox(out var boxCenter, out var boxHalfExtents, 
                        out var boxOrientation);
                    dstManager.AddComponentData(entity, new ItemColliderData{
                        ColliderType = ColliderType.Box,
                        BoxCenter = boxCenter - position,
                        BoxHalfExtents = boxHalfExtents,
                        BoxOrientation = boxOrientation,
                    });
                    break;
            }
        }

        private void TakeExperience() =>
            _heroExperienceLevel.PickUpItemBonus();

        private bool CanPickUp(IItem item) =>
            item.CanBePickedUpWithoutBackpack || 
            !item.CanBePickedUpWithoutBackpack && _heroInventory.HasPack;

        private bool PickUpItem(IItem item) {
            if (!item.CanBePickedUpWhenBackpackIsFull && _heroInventory.PackIsFull) return false;
            item.PickUpItem(_heroInventory);
            item.PickUpItem(_heroWeapon);
            return true;
        }

        private static void DestroyItem(ItemEntity item) {
            if (item.Entity != Entity.Null)
                item.EntityManager.AddComponentData(item.Entity, new DestroyComponentData());
            Destroy(item.gameObject);
        }
    }
}
using System.Collections.Generic;
using CodeBase.Hero.Interfaces;
using CodeBase.Items.ComponentData;
using CodeBase.Items.Interfaces;
using Sirenix.OdinInspector;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace CodeBase.Common
{
    public class CollisionAbility : MonoBehaviour, IAbility, IConvertGameObjectToEntity
    {
        [Required, SerializeField] private Collider Collider;
        [SerializeField] private List<MonoBehaviour> CollisionActions = new List<MonoBehaviour>();

        private readonly List<IAbility> _collisionActionsAbilities = new List<IAbility>();
        public List<Collider> Collisions { get; } = new List<Collider>();

        private void Start() {
            foreach (var action in CollisionActions) {
                if (action is IAbility ability)
                    _collisionActionsAbilities.Add(ability);
                else
                    Debug.Log("Collision action must derive from IAbility");
            }
        }

        public void Execute() {
            foreach (var action in _collisionActionsAbilities) {
                if (action is IAbilityTargets actionTarget) {
                    actionTarget.Targets = new List<GameObject>();
                    Collisions.ForEach( c => {
                        actionTarget.Targets.Add(c.gameObject);
                    });
                }
                action.Execute();
            }                
        }

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
    }
}
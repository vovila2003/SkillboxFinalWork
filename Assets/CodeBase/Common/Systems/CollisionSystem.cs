using System;
using CodeBase.Attack;
using CodeBase.Items;
using CodeBase.Items.ComponentData;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace CodeBase.Common.Systems
{
    public class CollisionSystem : ComponentSystem 
    {
        private readonly Collider[] _results = new Collider[50]; 

        protected override void OnUpdate() {
            Entities.WithAll<ItemColliderData, Transform>().ForEach(
                (Entity entity, CollisionAbility collisionAbility, ref ItemColliderData colliderData) => {
                    if (collisionAbility == null) return;
                    var gameObject = collisionAbility.gameObject;
                    float3 position = gameObject.transform.position;
                    var rotation = gameObject.transform.rotation;

                    Array.Clear(_results, 0, _results.Length);

                    switch (colliderData.ColliderType) {
                        case ColliderType.Sphere:
                            Physics.OverlapSphereNonAlloc(colliderData.SphereCenter + position, 
                                colliderData.SphereRadius, 
                                _results);
                            break;
                        case ColliderType.Capsule:
                            var center = (colliderData.CapsuleEnd + colliderData.CapsuleStart) / 2f + position; 
                            var point0 = colliderData.CapsuleStart + position;
                            var point1 = colliderData.CapsuleEnd + position;
                            point0 = (float3) (rotation * (point0 - center)) + center;
                            point1 = (float3) (rotation * (point1 - center)) + center;
                            Physics.OverlapCapsuleNonAlloc(point0, 
                                point1, 
                                colliderData.CapsuleRadius, 
                                _results);
                            break;
                        case ColliderType.Box:
                            Physics.OverlapBoxNonAlloc(colliderData.BoxCenter + position, 
                                colliderData.BoxHalfExtents, 
                                _results, 
                                colliderData.BoxOrientation * rotation);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    collisionAbility.Collisions.Clear();
                    
                    foreach (var collision in _results) {
                        if (collision == null) continue;
                        if (collision.gameObject != gameObject) 
                            collisionAbility.Collisions.Add(collision);
                    }
                    
                    var size = collisionAbility.Collisions.Count;

                    if (size > 0) {
                        collisionAbility.Execute();
                    }
                }
            );
        }
    }
}
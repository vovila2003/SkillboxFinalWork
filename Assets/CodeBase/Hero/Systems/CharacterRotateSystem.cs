using CodeBase.Hero.ComponentData;
using Unity.Entities;
using UnityEngine;

namespace CodeBase.Hero.Systems
{
    public partial class CharacterRotateSystem : SystemBase 
    {
        protected override void OnUpdate() {
            var deltaTime = Time.DeltaTime;

            Entities
                .WithAll<HeroTagData>()
                .WithChangeFilter<InputData>()
                .ForEach(
                    (HeroHealth health, Transform transform, ref MoveData move, in InputData inputData) => {
                        if (health.IsDead || transform == null) return;
                        
                        var vec = new Vector3(inputData.Move.x,
                            0,
                            inputData.Move.y);
                        
                        if (vec == Vector3.zero) return;

                        var rot = transform.rotation;
                        var newRot = Quaternion.LookRotation(Vector3.Normalize(vec));
                        if (rot != newRot) {
                            transform.rotation
                                = Quaternion.Lerp(rot, newRot, deltaTime * move.RotateSpeed);
                        }
                    }
            )
            .WithoutBurst()
            .Run();
        }
    }
}
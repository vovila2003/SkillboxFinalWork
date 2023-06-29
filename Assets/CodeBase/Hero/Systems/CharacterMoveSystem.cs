using CodeBase.Common;
using CodeBase.Hero.ComponentData;
using Unity.Entities;
using UnityEngine;

namespace CodeBase.Hero.Systems
{
    public partial class CharacterMoveSystem : SystemBase
    {
        protected override void OnUpdate() {
            Entities
                .WithAll<HeroTagData>()
                .WithChangeFilter<InputData>()
                .ForEach(
                    (HeroAnimator animator, HeroHealth health, in UserInput userInput, in InputData inputData) => {
                        if (health.IsDead || animator == null) return;
                        var isMoving = Mathf.Abs(inputData.Move.x) > Constants.Threshold ||
                                       Mathf.Abs(inputData.Move.y) > Constants.Threshold;
                        animator.Run(isMoving);
                    }
                )
                .WithoutBurst()
                .Run();
        }
    }
}
using CodeBase.Enemies.ComponentData;
using CodeBase.Enemies.Interfaces;
using Unity.Entities;

namespace CodeBase.Enemies.Systems
{
    public partial class AIEvaluateSystem : SystemBase
    {
        protected override void OnUpdate() {
            var deltaTime = Time.DeltaTime;
            
            Entities
                .WithAll<AIAgentData>()
                .ForEach(
                    (Entity entity, BehaviourManager manager, ref AIAgentData agentData) => {
                        if (manager == null) return;
                        if (agentData.CurrentTime > 0) {
                            var currentTime = agentData.CurrentTime;
                            currentTime -= deltaTime;
                            agentData.CurrentTime = currentTime;
                            return;
                        }

                        agentData.CurrentTime = agentData.EvaluateTime;
                        IBehaviour bestBehaviour = null;
                        var highScore = float.MinValue;
                        foreach (var behaviour in manager.CheckedBehaviours) {
                            if (behaviour == null) continue;
                            var currentScore = behaviour.Evaluate();
                            if (currentScore <= highScore) continue;
                            highScore = currentScore;
                            bestBehaviour = behaviour;
                        }

                        manager.ActiveBehaviour = bestBehaviour;
                    }
                )
                .WithoutBurst()
                .Run();
        }
    }
}
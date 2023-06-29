using CodeBase.Enemies.ComponentData;
using Unity.Entities;

namespace CodeBase.Enemies.Systems
{
    public partial class AIBehaveSystem : SystemBase
    {
        protected override void OnUpdate() {
            Entities
                .WithAll<AIAgentData>()
                .ForEach(
                    (Entity entity, in BehaviourManager manager) => {
                        if (manager != null)
                            manager.ActiveBehaviour?.Behave();
                    }
                )
                .WithoutBurst()
                .Run();
        }
    }
}
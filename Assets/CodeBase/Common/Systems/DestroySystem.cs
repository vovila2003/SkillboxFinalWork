using CodeBase.Common.ComponentData;
using CodeBase.Items.ComponentData;
using Unity.Entities;

namespace CodeBase.Common.Systems
{
    public partial class DestroySystem : SystemBase
    {
        private EndSimulationEntityCommandBufferSystem _endSimulationEcbSystem;

        protected override void OnCreate() {
            base.OnCreate();
            _endSimulationEcbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate() {
            var ecb = _endSimulationEcbSystem.CreateCommandBuffer().AsParallelWriter();

            Entities
                .WithAll<DestroyComponentData>()
                .ForEach(
                    (Entity entity, int entityInQueryIndex) => {
                        ecb.DestroyEntity(entityInQueryIndex, entity);
                    })
                .ScheduleParallel();
            
            _endSimulationEcbSystem.AddJobHandleForProducer(this.Dependency);
        }
    }
}
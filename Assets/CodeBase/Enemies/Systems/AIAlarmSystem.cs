using CodeBase.Enemies.ComponentData;
using Unity.Entities;

namespace CodeBase.Enemies.Systems
{
    public partial class AIAlarmSystem : SystemBase
    {
        protected override void OnUpdate() {
            var deltaTime = Time.DeltaTime;
            
            Entities
                .WithAll<AlarmData>()
                .ForEach(
                    (Entity entity, EnemyAnimator animator, ref AlarmData alarmData) => {
                        if (animator == null) return;
                        if (alarmData.CurrentTime > 0) {
                            var currentTime = alarmData.CurrentTime;
                            currentTime -= deltaTime;
                            alarmData.CurrentTime = currentTime;
                        }
                        else {
                            animator.Alarm(false);
                        }
                    }
                )
                .WithoutBurst()
                .Run();
        }
    }
}
using Unity.Entities;

namespace CodeBase.Enemies.ComponentData
{
    public struct AlarmData : IComponentData
    {
        public float AlarmTime;
        public float CurrentTime;
    }
}
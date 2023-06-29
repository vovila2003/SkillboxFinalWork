using Unity.Entities;

namespace CodeBase.Enemies.ComponentData
{
    public struct AIAgentData : IComponentData
    {
        public float EvaluateTime;
        public float CurrentTime;
    }
}
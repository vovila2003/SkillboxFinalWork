using CodeBase.Common;
using Unity.Entities;
using Unity.Mathematics;

namespace CodeBase.Items.ComponentData
{
    public struct ItemColliderData : IComponentData {
        public ColliderType ColliderType;
        public float3 SphereCenter;
        public float SphereRadius;
        public float3 CapsuleStart;
        public float3 CapsuleEnd;
        public float CapsuleRadius;
        public float3 BoxCenter;
        public float3 BoxHalfExtents;
        public quaternion BoxOrientation;
    }
}
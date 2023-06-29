using CodeBase.Items.ComponentData;
using Unity.Entities;
using UnityEngine;

namespace CodeBase.Items
{
    public class ItemEntity : MonoBehaviour, IConvertGameObjectToEntity
    {
        public EntityManager EntityManager { get; private set; }
        public Entity Entity { get; private set; }
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
            Entity = entity;
            EntityManager = dstManager;
            dstManager.AddComponentData(entity, new ItemData());
        }
    }
}
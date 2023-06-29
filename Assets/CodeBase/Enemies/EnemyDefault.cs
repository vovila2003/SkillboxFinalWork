using CodeBase.Common.Interfaces;
using CodeBase.Enemies.ComponentData;
using CodeBase.Enemies.Interfaces;
using CodeBase.Infrastructure.GameData;
using CodeBase.Items;
using Unity.Entities;
using UnityEngine;

namespace CodeBase.Enemies
{
    public abstract class EnemyDefault : MonoBehaviour, IConvertGameObjectToEntity, IEnemyType, IHasWeapon
    {
        protected Settings Settings;
        protected float AlarmTime;
        protected EnemyAnimator Animator;

        public float AttackDistance { get; protected set; } 
            
        public float PurseFromDistance { get; protected set; } 
        public float RotationSpeed { get; protected set; } 
        public float ShootDelay { get; protected set; }  
        public float EvaluateTime { get; protected set; } 
        public float Armor { get; protected set; } 
        public float Health { get; protected set; } 
        public ItemType Item1 { get; protected set; } 
        public ItemType Item2 { get; protected set; } 
        public Entity Entity { get; private set; }
        public EntityManager EntityManager { get; private set; }
        public IWeaponComponent WeaponComponent { get; private set; }
        
        private void Awake() {
            WeaponComponent = GetComponent<EnemyWeapon>();
            Animator = GetComponent<EnemyAnimator>();
        }

        public void SetupAlarm() {
            var alarmData = EntityManager.GetComponentData<AlarmData>(Entity);
            alarmData.CurrentTime = alarmData.AlarmTime;
            EntityManager.SetComponentData(Entity, alarmData);
        }

        public abstract void BehaveAttack();

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
            Entity = entity;
            EntityManager = dstManager;
            dstManager.AddComponentData(entity, new EnemyTagData());
            dstManager.AddComponentData(entity, new AlarmData {
                AlarmTime = AlarmTime,
                CurrentTime = 0
            });
        }
    }
}
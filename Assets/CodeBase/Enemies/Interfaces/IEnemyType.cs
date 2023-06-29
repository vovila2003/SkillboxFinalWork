using CodeBase.Items;
using Unity.Entities;

namespace CodeBase.Enemies.Interfaces
{
    public interface IEnemyType
    {
        void SetupAlarm();
        void BehaveAttack();
        float AttackDistance { get; }
        float PurseFromDistance { get; }
        float RotationSpeed { get; }
        float ShootDelay { get; }
        float EvaluateTime { get; }
        float Armor { get; }
        float Health { get; }
        ItemType Item1 { get; }
        ItemType Item2 { get; }
        Entity Entity { get; }
        EntityManager EntityManager { get; }
    }
}
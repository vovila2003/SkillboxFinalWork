using CodeBase.Enemies;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories.Interfaces
{
    public interface IEnemyFactory
    {
        void Create(Vector3 at, EnemyType type, Transform hero, Camera camera);
        void DestroyAllEnemies();
    }
}
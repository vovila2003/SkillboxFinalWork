using System;
using System.Collections.Generic;
using CodeBase.Enemies;
using CodeBase.Infrastructure.Factories.Interfaces;
using CodeBase.Infrastructure.GameData.Interfaces;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure.Factories
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly DiContainer _diContainer;
        private readonly IPrefabs _prefabs;
        private readonly List<GameObject> _enemies = new List<GameObject>();

        public EnemyFactory(DiContainer diContainer, IPrefabs prefabs) {
            _diContainer = diContainer;
            _prefabs = prefabs;
        }

        public void Create(Vector3 at, EnemyType type, Transform hero, Camera camera) {
            var prefab = EnemyPrefabByType(type);
            var enemy = Object.Instantiate(prefab, at, Quaternion.Euler(0, 0, -1));
            SetupEnemy(enemy, hero, camera);
            _enemies.Add(enemy);
        }

        public void DestroyAllEnemies() {
            foreach (var enemy in _enemies) {
                if (enemy == null) continue;
                Object.Destroy(enemy);
                var enemyHealth = enemy.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                    enemyHealth.DestroyEnemyUi();
            }
        }

        private void SetupEnemy(GameObject enemy, Transform hero, Camera camera) {
            _diContainer.Inject(enemy);
            var enemyTag = enemy.GetComponent<Enemy>();
            if (enemyTag == null) return;
            enemyTag.Hero = hero;
        }

        private GameObject EnemyPrefabByType(EnemyType type) =>
            type switch {
                EnemyType.MeleeLight => _prefabs.MeleeLightPrefab,
                EnemyType.MeleeHeavy => _prefabs.MeleeHeavyPrefab,
                EnemyType.Ranged => _prefabs.RangedPrefab,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
    }
}
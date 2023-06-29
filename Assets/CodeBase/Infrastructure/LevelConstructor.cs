using System.Collections.Generic;
using CodeBase.Infrastructure.Factories.Interfaces;
using CodeBase.Infrastructure.Markers;
using CodeBase.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class LevelConstructor : MonoBehaviour
    {
        [Required, SerializeField] private Transform StartPoint;
        [SerializeField] private List<EnemyPoint> EnemyPoints;
        [SerializeField] private List<ItemPoint> ItemPoints;
        [Required, SerializeField] private ResultScreen ResultScreen;
        [Required, SerializeField] private Camera Camera;

        private IHeroFactory _heroFactory;
        private IEnemyFactory _enemyFactory;
        private IItemFactory _itemFactory;

        [Inject]
        private void Construct(IHeroFactory heroFactory,
                               IEnemyFactory enemyFactory,
                               IItemFactory itemFactory) {
            _heroFactory = heroFactory;
            _enemyFactory = enemyFactory;
            _itemFactory = itemFactory;
        }

        public void InitGameWorld() {
            var hero = _heroFactory.Create(StartPoint.position);
            CreateEnemies(hero);
            CreateItems();
            var game = Bootstrapper.Instance.Game; 
            game.Pause = Instantiate(game.Prefabs.PauseMenu).GetComponent<PauseMenu>();
            game.Result = ResultScreen;
            ResultScreen.Register(_heroFactory);
            ResultScreen.Register(_enemyFactory);
        }

        private void CreateItems() {
            foreach (var point in ItemPoints) {
                _itemFactory.Create(point.transform.position, point.ItemType);
            }
        }

        private void CreateEnemies(Transform hero) {
            foreach (var point in EnemyPoints) {
                _enemyFactory.Create(point.transform.position, point.EnemyType, hero, Camera);
            }
        }
    }
}
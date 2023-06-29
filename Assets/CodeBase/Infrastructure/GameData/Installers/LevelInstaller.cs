using CodeBase.GraphicEffects;
using CodeBase.Infrastructure.Factories;
using CodeBase.Infrastructure.Factories.Interfaces;
using CodeBase.Infrastructure.GameData.Interfaces;
using CodeBase.Infrastructure.GameData.Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.GameData.Installers
{
    public class LevelInstaller : MonoInstaller, IInitializable
    {
        [Required, SerializeField] private LevelConstructor LevelConstructor;
        
        private Game _game;
        private Settings _settings;
        private IPrefabs _prefabs;

        [Inject]
        private void Construct(Settings settings) => 
            _settings = settings;

        public override void InstallBindings() {
            Init();
            BindInterfaces();
            BindPrefabs();
            BindEffectsPools();
            BindSignalBus();
            BindSignals();
            BindFactories();
        }

        public void Initialize() {
            LevelConstructor.InitGameWorld();
        }

        private void Init() {
            _game = Bootstrapper.Instance.Game;
            _prefabs = _game.Prefabs;
        }

        private void BindInterfaces() {
            Container.BindInterfacesTo<LevelInstaller>()
                     .FromInstance(this);
        }

        private void BindPrefabs() {
            Container.Bind<IPrefabs>()
                     .To<Prefabs>()
                     .FromInstance(_game.Prefabs)
                     .AsSingle()
                     .NonLazy();
        }

        private void BindEffectsPools() {
            Container.BindMemoryPool<PistolFireEffect, PistolFireEffect.Pool>()
                     .WithInitialSize(_settings.PistolFireEffectPoolSize)
                     .ExpandByOneAtATime()
                     .FromComponentInNewPrefab(_prefabs.PistolFireEffectPrefab)
                     .UnderTransformGroup("Effects");
            
            Container.BindMemoryPool<GunFireEffect, GunFireEffect.Pool>()
                     .WithInitialSize(_settings.GunFireEffectPoolSize)
                     .ExpandByOneAtATime()
                     .FromComponentInNewPrefab(_prefabs.GunFireEffectPrefab)
                     .UnderTransformGroup("Effects");
            
            Container.BindMemoryPool<GunFireEnemyEffect, GunFireEnemyEffect.Pool>()
                     .WithInitialSize(_settings.GunFireEnemyEffectPoolSize)
                     .ExpandByOneAtATime()
                     .FromComponentInNewPrefab(_prefabs.GunFireEnemyOneShotEffectPrefab)
                     .UnderTransformGroup("Effects");
            
            Container.BindMemoryPool<BloodEffect, BloodEffect.Pool>()
                     .WithInitialSize(_settings.BloodEffectPoolSize)
                     .ExpandByOneAtATime()
                     .FromComponentInNewPrefab(_prefabs.BloodEffectPrefab)
                     .UnderTransformGroup("Effects");
            
            Container.BindMemoryPool<BulletHitEffect, BulletHitEffect.Pool>()
                     .WithInitialSize(_settings.BulletHitEffectPoolSize)
                     .ExpandByOneAtATime()
                     .FromComponentInNewPrefab(_prefabs.BulletHitEffectPrefab)
                     .UnderTransformGroup("Effects");
        }

        private void BindSignalBus() => 
            SignalBusInstaller.Install(Container);

        private void BindSignals() {
            Container.DeclareSignal<EnemyKilledSignal>();
        }

        private void BindFactories() {
            Container.Bind<IHeroFactory>()
                     .To<HeroFactory>()
                     .AsSingle()
                     .NonLazy();
            
            Container.Bind<IEnemyFactory>()
                     .To<EnemyFactory>()
                     .AsSingle()
                     .NonLazy();

            Container.Bind<IItemFactory>()
                     .To<ItemFactory>()
                     .AsSingle()
                     .NonLazy();
        }
    }
}
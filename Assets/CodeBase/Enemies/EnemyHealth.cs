using CodeBase.Common;
using CodeBase.Common.ComponentData;
using CodeBase.Common.Interfaces;
using CodeBase.Enemies.Interfaces;
using CodeBase.Infrastructure.Factories.Interfaces;
using CodeBase.Infrastructure.GameData;
using CodeBase.Infrastructure.GameData.Signals;
using CodeBase.Weapon.Interfaces;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace CodeBase.Enemies
{
    [RequireComponent(typeof(EnemyArmor), typeof(EnemyAnimator))]
    [RequireComponent(typeof(EnemySound))]
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [Required, SerializeField] private EnemyUi EnemyUi;
        private float _threshold1;
        private float _threshold2;
        private float _health;
        private IEnemyAnimator _enemyAnimator;
        private EnemyArmor _enemyArmor;
        private IEnemyType _enemyType;
        private SignalBus _signalBus;
        private Settings _settings;
        private int _destroyDelay;
        private IItemFactory _itemFactory;
        private EnemySound _enemySound;

        [ShowInInspector]
        public bool IsDead { get; private set; }

        [ShowInInspector]
        private float Current { 
            get => _health;
            set {
                if (Mathf.Abs(_health - value) < Constants.Threshold) return;
                _health = value;
                if (!(_health <= 0) || IsDead) return;
                EnemyDie();
            }
        }

        [Inject]
        private void Construct(SignalBus signalBus, Settings settings, IItemFactory itemFactory) {
            _signalBus = signalBus;
            _settings = settings;
            _itemFactory = itemFactory;
        }

        private void Awake() {
            _enemyAnimator = GetComponent<EnemyAnimator>();
            _enemyArmor = GetComponent<EnemyArmor>();
            _enemyType = GetComponent<IEnemyType>();
            _enemySound = GetComponent<EnemySound>();
        }

        private void Start() {
            Current = _enemyType.Health;
            IsDead = false;
            _destroyDelay = _settings.EnemyDestroyAfterDieDelayMs;
            _threshold1 = _settings.CreateItemAfterEnemyDieThreshold1;
            _threshold2 = _settings.CreateItemAfterEnemyDieThreshold2;
            UpdateEnemyUi();
        }

        public void Damage(IWeapon weapon) {
            var weaponSettings = weapon.WeaponSettings;
            var armorDamage = weaponSettings.Damage * (1 - weaponSettings.ArmorPenetration);
            if (_enemyArmor.Current >= armorDamage) {
                _enemyArmor.Current -= armorDamage;
                Current -= weaponSettings.Damage * weaponSettings.ArmorPenetration;
            }
            else {
                Current -= weaponSettings.Damage - _enemyArmor.Current;
                _enemyArmor.Current = 0;
            }
            UpdateEnemyUi();
            _enemySound.PlayDamage();
            if (!IsDead)
                _enemyAnimator.Damage();
        }

        public void DestroyEnemyUi() => 
            Destroy(EnemyUi.gameObject);

        private async void EnemyDie() {
            IsDead = true;
            _enemyAnimator.Die();
            _enemySound.StopSpeech();
            _enemySound.PlayDeath();
            _signalBus?.Fire<EnemyKilledSignal>();
            RemoveCollider();
            CreateItem();
            DestroyEnemyUi();
            await UniTask.Delay(_destroyDelay);
            DestroyEnemy();
        }

        private void CreateItem() {
            var value = Random.Range(0f, 1f);
            if (value > _threshold2) return;
            var itemType = _enemyType.Item1;
            if (value > _threshold1) 
                itemType = _enemyType.Item2;
            _itemFactory.Create(transform.position, itemType);
        }

        private void DestroyEnemy() {
            var entityExists = _enemyType.EntityManager.UniversalQuery.GetEntityQueryMask();
            if (entityExists.Matches(_enemyType.Entity))
                _enemyType.EntityManager.AddComponentData(_enemyType.Entity, new DestroyComponentData());
            Destroy(gameObject);
        }

        private void RemoveCollider() => 
            Destroy(GetComponent<CapsuleCollider>());

        private void UpdateEnemyUi() {
            EnemyUi.ChangeHealth(Current / _enemyType.Health);
            EnemyUi.ChangeArmor(_enemyArmor.Current / 100);
        }
    }
}
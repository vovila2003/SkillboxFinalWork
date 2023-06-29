using CodeBase.Common.Interfaces;
using CodeBase.GraphicEffects;
using CodeBase.Infrastructure.GameData;
using CodeBase.Items;
using CodeBase.Weapon.Interfaces;
using Zenject;

namespace CodeBase.Enemies
{
    public class EnemyRanged : EnemyDefault, IBulletAttack
    {
        private IFireArms _fireArms;
        private GunFireEnemyEffect.Pool _pool; 

        public float Accuracy => Settings.RangedShootAccuracy;

        [Inject]
        private void Construct(Settings settings, GunFireEnemyEffect.Pool pool) {
            Settings = settings;
            _pool = pool;
            AlarmTime = Settings.RangedAlarmTime;
            AttackDistance = Settings.RangedAttackDistance;
            PurseFromDistance = Settings.RangedPurseFromDistance;
            RotationSpeed = Settings.RangedRotationSpeed;
            ShootDelay = Settings.RangedShootDelay;
            EvaluateTime = Settings.RangedEvaluateTime;
            Armor = Settings.RangedArmor;
            Health = Settings.RangedHealth;
            Item1 = ItemType.BulletsPack;
            Item2 = ItemType.Gun;
        }

        private void Start() {
            _fireArms = WeaponComponent.CurrentWeapon as IFireArms;
            _fireArms?.RegisterPool(_pool);
        }

        public override void BehaveAttack() {
            if (_fireArms.CanShoot) {
                Animator.Attack();
                _fireArms.Attack();
            } else {
                Animator.Reload();
                _fireArms.Reload();
            }
        }
    }
}
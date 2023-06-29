using CodeBase.Hero.Interfaces;
using CodeBase.Infrastructure.GameData;
using CodeBase.Items;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace CodeBase.Enemies
{
    public class EnemyMeleeHeavy : EnemyDefault
    {
        [Required, SerializeField] private MonoBehaviour AttackAbility;
        
        private IAbility _checkedAttackAbility;
        
        [Inject]
        private void Construct(Settings settings) {
            Settings = settings;
            AlarmTime = Settings.MeleeHeavyAlarmTime;
            AttackDistance = Settings.MeleeHeavyAttackDistance;
            PurseFromDistance = Settings.MeleeHeavyPurseFromDistance;
            RotationSpeed = Settings.MeleeHeavyRotationSpeed;
            ShootDelay = Settings.MeleeHeavyShootDelay;
            EvaluateTime = Settings.MeleeHeavyEvaluateTime;
            Armor = Settings.MeleeHeavyArmor;
            Health = Settings.MeleeHeavyHealth;
            Item1 = ItemType.ArmorBox;
            Item2 = ItemType.HealthBox;
        }

        private void Start() {
            if (AttackAbility is IAbility ability)
                _checkedAttackAbility = ability;
            else {
                Debug.Log("AttackAbility must derive from IAbility");
            }
        }

        public override async void BehaveAttack() {
            Animator.Attack();
            await UniTask.WaitUntil(() => Animator.IsAttack);
            _checkedAttackAbility.Execute();
        }
    }
}
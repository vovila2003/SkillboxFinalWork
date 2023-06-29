using CodeBase.Hero.Interfaces;
using CodeBase.Infrastructure.GameData;
using CodeBase.Items;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace CodeBase.Enemies
{
    public class EnemyMeleeLight : EnemyDefault
    {
        [Required, SerializeField] private MonoBehaviour AttackAbility;
        
        private IAbility _checkedAttackAbility;
        
        [Inject]
        private void Construct(Settings settings) {
            Settings = settings;
            AlarmTime = Settings.MeleeLightAlarmTime;
            AttackDistance = Settings.MeleeLightAttackDistance;
            PurseFromDistance = Settings.MeleeLightPurseFromDistance;
            RotationSpeed = Settings.MeleeLightRotationSpeed;
            ShootDelay = Settings.MeleeLightShootDelay;
            EvaluateTime = Settings.MeleeLightEvaluateTime;
            Armor = Settings.MeleeLightArmor;
            Health = Settings.MeleeLightHealth;
            Item1 = ItemType.HealthBox;
            Item2 = ItemType.Pistol;
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
using CodeBase.Enemies.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Enemies
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimator : MonoBehaviour, IEnemyAnimator
    {
        private readonly int _runHash = Animator.StringToHash("Run"); 
        private readonly int _attackHash = Animator.StringToHash("Attack"); 
        private readonly int _damageHash = Animator.StringToHash("Damage"); 
        private readonly int _dieHash = Animator.StringToHash("Die");
        private readonly int _randomHash = Animator.StringToHash("Random");
        private readonly int _alarmHash = Animator.StringToHash("Alarm");
        private readonly int _reloadHash = Animator.StringToHash("Reload");
        
        private Animator _animator;
        private IEnemyType _enemyType;

        public Vector3 DeltaPosition => _animator.deltaPosition;
        public bool IsAttack => 
            _animator != null && _animator.GetCurrentAnimatorStateInfo(0).IsName("attack");

        private void Awake() {
            _animator = GetComponent<Animator>();
            _enemyType = GetComponent<IEnemyType>();
        }

        public void Run(bool isRun) =>
            _animator.SetBool(_runHash, isRun);

        public void Alarm(bool isAlarm) =>
            _animator.SetBool(_alarmHash, isAlarm);

        public void Attack() =>
            _animator.SetTrigger(_attackHash);
        
        public void Damage() {
            _animator.SetTrigger(_damageHash);
            _enemyType.SetupAlarm();
            Alarm(true);
        }

        public void Die() {
            var random = Random.Range(0, 4);
            _animator.SetInteger(_randomHash, random);
            _animator.SetTrigger(_dieHash);
        }

        public void Reload() =>
            _animator.SetTrigger(_reloadHash);
    }
}
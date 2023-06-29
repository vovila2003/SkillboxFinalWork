using CodeBase.Common;
using CodeBase.Enemies.Interfaces;
using CodeBase.Hero;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemies.Behaviours
{
    [RequireComponent(typeof(EnemyAnimator), typeof(Enemy))]
    [RequireComponent(typeof(NavMeshAgent), typeof(EnemyHealth), typeof(EnemySound))]
    public class AttackBehaviour : MonoBehaviour, IBehaviour
    {
        private EnemyAnimator _animator;
        private EnemyHealth _enemyHealth;
        private float _attackDistance;
        private float _distanceToHero;
        private Transform _transform;
        private Transform _heroTransform;
        private NavMeshAgent _agent;
        private float _rotationSpeed;
        private float _shootTime = float.MinValue;
        private float _shootDelay;
        private HeroHealth _heroHealth;
        private EnemySound _enemySound;
        private int _layer;
        private float _enemyHeight;
        private float _heroHeight;
        private IEnemyType _enemyType;


        private void Awake() {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<EnemyAnimator>();
            _enemyHealth = GetComponent<EnemyHealth>();
            _transform = transform;
            _enemySound = GetComponent<EnemySound>();
            _enemyType = GetComponent<IEnemyType>();
        }

        private void Start() {
            _attackDistance = _enemyType.AttackDistance;
            _rotationSpeed = _enemyType.RotationSpeed;
            _shootDelay = _enemyType.ShootDelay;
            _heroTransform = GetComponent<Enemy>().Hero;
            _heroHeight = _heroTransform.GetComponent<CapsuleCollider>().height / 2;
            _enemyHeight = GetComponent<CapsuleCollider>().height / 2;
            _layer = (1 << LayerMask.NameToLayer(Constants.HeroLayerName)) | (1 << LayerMask.NameToLayer(Constants.DefaultLayerName));
        }

        public float Evaluate() {
            const int minPriority = Constants.MinPriority;
            if (CheckHeroHealth()) return minPriority;
            if (_heroHealth.IsDead) return minPriority;
            UpdateDistanceToHero();
            if (_distanceToHero > _attackDistance) return minPriority;
            if (!InVision()) return minPriority;
            Alarm();
            return Constants.AttackPriority;
        }

        public void Behave() {
            if (_enemyHealth.IsDead) return;
            _enemySound.PlaySpeech();
            _agent.isStopped = true;
            _animator.Run(false);
            RotateEnemyToTarget();
            if (!TimeToShoot()) return;
            _enemyType.BehaveAttack();
        }

        private bool CheckHeroHealth() {
            if (_heroHealth == null)
                _heroHealth = _heroTransform.GetComponent<HeroHealth>();
            return _heroHealth == null;
        }

        private bool TimeToShoot() {
            var time = Time.time;
            if (time < _shootTime + _shootDelay) return false;
            _shootTime = time;
            return true;
        }

        private void UpdateDistanceToHero() {
            if (_transform != null && _heroTransform != null)
                _distanceToHero = Vector3.Distance(_transform.position, _heroTransform.position);
        }

        private bool InVision() {
            if (_agent == null || _heroTransform == null) return false;
            var startPosition = _transform.position + _transform.up * _enemyHeight;
            var finishPosition = _heroTransform.position + _heroTransform.up * _heroHeight;
            RaycastHit hit;
            var raycast = Physics.Raycast(startPosition,
                (finishPosition - startPosition).normalized,
                out hit,
                _attackDistance,
                _layer,
                QueryTriggerInteraction.Ignore);

            return raycast && hit.transform.tag.Equals(Constants.HeroTag);
        }

        private void Alarm() {
            _enemyType.SetupAlarm();
            _animator.Alarm(true);
        }

        private void RotateEnemyToTarget() {
            if (_heroTransform == null || _transform == null) return;
            var targetDirection = _heroTransform.position - _transform.position;
            targetDirection.y = 0;
            if (targetDirection == Vector3.zero) return;
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation,
                Quaternion.LookRotation(targetDirection), _rotationSpeed * Time.deltaTime);
        }
    }
}
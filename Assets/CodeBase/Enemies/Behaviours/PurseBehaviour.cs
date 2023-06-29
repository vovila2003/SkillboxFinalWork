using System;
using CodeBase.Common;
using CodeBase.Enemies.Interfaces;
using CodeBase.Hero;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemies.Behaviours
{
    [RequireComponent(typeof(NavMeshAgent), typeof(EnemyAnimator) ,typeof(Enemy))]
    [RequireComponent(typeof(EnemyHealth), typeof(EnemySound))]
    public class PurseBehaviour : MonoBehaviour, IBehaviour
    {
        private NavMeshAgent _agent;
        private EnemyAnimator _animator;
        private EnemyHealth _enemyHealth;
        private float _attackDistance;
        private float _purseFromDistance;
        private float _rotationSpeed;
        private Transform _transform;
        private Transform _heroTransform;
        private float _distanceToHero;
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
            _purseFromDistance = _enemyType.PurseFromDistance;
            CheckDistances();
            _agent.autoBraking = true;
            _agent.autoRepath = true;
            _agent.stoppingDistance = _attackDistance * 0.5f;
            _rotationSpeed = _enemyType.RotationSpeed;
            _heroTransform = GetComponent<Enemy>().Hero;
            _heroHeight = _heroTransform.GetComponent<CapsuleCollider>().height / 2 * _heroTransform.lossyScale.y;
            _enemyHeight = GetComponent<CapsuleCollider>().height / 2 * _transform.lossyScale.y;
            _layer = (1 << LayerMask.NameToLayer(Constants.HeroLayerName)) | (1 << LayerMask.NameToLayer(Constants.DefaultLayerName));
        }

        public float Evaluate() {
            const int minPriority = Constants.MinPriority;
            if (CheckHeroHealth()) return minPriority;
            if (_heroHealth.IsDead) return minPriority;
            UpdateDistanceToHero();
            if (_distanceToHero > _purseFromDistance) return minPriority;
            if (!InVision()) return AgentHasPath() ? Constants.PursePriority : minPriority;
            Alarm();
            return Constants.PursePriority;
        }

        public void Behave() {
            if (_enemyHealth.IsDead) return;
            _enemySound.PlaySpeech();
            UpdateDistanceToHero();
            if (_distanceToHero < _attackDistance) {
                _agent.isStopped = true;
                _animator.Run(false);
                RotateEnemyToTarget();
                return;
            }
            
            if (InVision()) 
                _agent.SetDestination(_heroTransform.position);

            _agent.isStopped = false;
            _animator.Run(true);
            RotateEnemyToTarget();
        }

        private bool CheckHeroHealth() {
            if (_heroHealth == null)
                _heroHealth = _heroTransform.GetComponent<HeroHealth>();
            return _heroHealth == null;
        }

        private void OnAnimatorMove() {
            var deltaTime = Time.deltaTime;
            if (deltaTime < Constants.Threshold) return;
            _agent.velocity = _animator.DeltaPosition / deltaTime;
        }
       
        private void CheckDistances() {
            if (Mathf.Abs(_purseFromDistance - _attackDistance) < Constants.Threshold || 
                _attackDistance > _purseFromDistance ||
                _purseFromDistance < 0 ||
                _attackDistance < 0)
                throw new ArgumentOutOfRangeException(nameof(_purseFromDistance), "AttackDistance must be <= PusreDistanceFrom");
        }

        private bool InVision() {
            if (_agent == null || _heroTransform == null) return false;
            var startPosition = _transform.position + _transform.up * _enemyHeight;
            var finishPosition = _heroTransform.position + _heroTransform.up * _heroHeight;
            RaycastHit hit;
            var raycast = Physics.Raycast(startPosition,
                (finishPosition - startPosition).normalized,
                out hit,
                _purseFromDistance,
                _layer,
                QueryTriggerInteraction.Ignore);

            return raycast && hit.transform.tag.Equals(Constants.HeroTag);
        }

        private bool AgentHasPath() =>
            _agent!= null 
            && _agent.hasPath
            && _agent.pathStatus == NavMeshPathStatus.PathComplete
            && _agent.remainingDistance > _attackDistance;

        private void UpdateDistanceToHero() {
            if (_transform != null && _heroTransform != null)
                _distanceToHero = Vector3.Distance(_transform.position, _heroTransform.position);
        }

        private void RotateEnemyToTarget() {
            if (_agent == null || _transform == null) return;
            var targetDirection = _agent.steeringTarget - _transform.position;
            targetDirection.y = 0;
            if (targetDirection == Vector3.zero) return;
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation,
                Quaternion.LookRotation(targetDirection), _rotationSpeed * Time.deltaTime);
        }
        private void Alarm() {
            _enemyType.SetupAlarm();
            _animator.Alarm(true);
        }
    }
}
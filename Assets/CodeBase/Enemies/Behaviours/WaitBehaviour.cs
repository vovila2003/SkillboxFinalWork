using CodeBase.Common;
using CodeBase.Enemies.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemies.Behaviours
{
    [RequireComponent(typeof(NavMeshAgent), typeof(EnemyAnimator), typeof(EnemySound))]
    public class WaitBehaviour : MonoBehaviour, IBehaviour
    {
        private NavMeshAgent _agent;
        private EnemyAnimator _animator;
        private EnemySound _enemySound;

        private void Awake() {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<EnemyAnimator>();
            _enemySound = GetComponent<EnemySound>();
        }

        public float Evaluate() => Constants.WaitPriority;

        public void Behave() {
            _enemySound.StopSpeech();
            if (_agent != null)
                _agent.isStopped = true;
            if (_animator != null)
                _animator.Run(false);
        }
    }
}
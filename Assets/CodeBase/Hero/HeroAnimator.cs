using CodeBase.Hero.Interfaces;
using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(Animator))]
    public class HeroAnimator : MonoBehaviour, IHeroAnimator
    {
        private readonly int _runHash = Animator.StringToHash("Run");
        private readonly int _attackHash = Animator.StringToHash("Attack");
        private readonly int _reloadHash = Animator.StringToHash("Reload");
        private readonly int _weaponHash = Animator.StringToHash("Weapon");
        private readonly int _dieHash = Animator.StringToHash("Die");
        private readonly int _interactHash = Animator.StringToHash("Interact");

        private Animator _animator;
        private int _attackLayerIndex;
        
        private void Awake() => 
            _animator = GetComponent<Animator>();

        private void Start() => 
            _attackLayerIndex = _animator.GetLayerIndex("AttackReloadLayer");

        public void Run(bool isRun) => 
            _animator.SetBool(_runHash, isRun);

        public void Attack() {
            SwitchOnAttackReloadLayer();
            _animator.SetTrigger(_attackHash);
        }

        public void Reload() {
            SwitchOnAttackReloadLayer();
            _animator.SetTrigger(_reloadHash);
        }

        public void Weapon(int weapon) =>
            _animator.SetInteger(_weaponHash, weapon);

        public void Die() {
            SwitchOffAttackReloadLayer();
            _animator.SetTrigger(_dieHash);
        }

        public void Interact() =>
            _animator.SetTrigger(_interactHash);

        public void ResetInteractTrigger() => 
            _animator.ResetTrigger(_interactHash);

        private void SwitchOnAttackReloadLayer() => 
            _animator.SetLayerWeight(_attackLayerIndex, 1);

        private void SwitchOffAttackReloadLayer() => 
            _animator.SetLayerWeight(_attackLayerIndex, 0);
    }
}
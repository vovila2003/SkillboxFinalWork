using System;
using CodeBase.Common;
using CodeBase.Common.Interfaces;
using CodeBase.GraphicEffects;
using CodeBase.Hero.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Settings = CodeBase.Infrastructure.GameData.Settings;

namespace CodeBase.Attack
{
    public class KnifeAttackAbility : MonoBehaviour, IAbility
    {
        private float _knifeAttackDistance;
        private float _radius;
        private readonly RaycastHit[] _results = new RaycastHit[5];
        private int _layerTarget;
        private Settings _settings;
        private Transform _transform;
        private BloodEffect.Pool _bloodPool;
        private IWeaponComponent _weaponComponent;
        private ICharacter _character;
        private IHasWeapon _hasWeapon;

        //------------------------------
        // private bool _hitDetect;
        // private RaycastHit _hitInfo;
        //
        //------------------------------

        [Inject]
        private void Construct(Settings settings,
                               BloodEffect.Pool bloodPool) {
            _settings = settings;
            _bloodPool = bloodPool;
        }

        private void Awake() {
            _transform = transform;
            _character = GetComponent<ICharacter>();
            _hasWeapon = GetComponent<IHasWeapon>();
        }

        private void Start() {
            _weaponComponent = _hasWeapon.WeaponComponent;
            _layerTarget = _character.LayerTarget; 
            _radius = _settings.KnifeRadius;
            _knifeAttackDistance = _settings.KnifeAttackDistance;
        }

        // private void FixedUpdate() {
        //     var pos = _transform.position;
        //     var right = _transform.right;
        //     var startPosition = new Vector3(pos.x, Constants.KnifeHeight + pos.y, pos.z) + right * Constants.StartPointXOffset;
        //     var direction = _transform.forward - right * Constants.DirectionXOffset;
        //     _hitDetect = Physics.SphereCast(new Ray(startPosition, direction), _radius, out _hitInfo, _knifeAttackDistance, _layerTarget);
        // }

        public async void Execute() {
            await UniTask.Delay(Constants.KnifeAttackDelayMs);
            var pos = _transform.position;
            var right = _transform.right;
            var startPosition = new Vector3(pos.x, Constants.KnifeHeight + pos.y, pos.z) + right * Constants.StartPointXOffset;
            var direction = (_transform.forward - right * Constants.DirectionXOffset).normalized;
            ClearArray();
            Physics.SphereCastNonAlloc(startPosition, _radius, direction, _results,
                _knifeAttackDistance, _layerTarget, QueryTriggerInteraction.Ignore);
            foreach (var hit in _results) {
                if (hit.collider == null) continue;
                if (ApplyDamage(hit)) return;
            }
            PlayKnifeSound(false);
        }

        private bool ApplyDamage(RaycastHit hit) {
            var targetHealth = hit.transform.GetComponent<IHealth>();
            if (targetHealth == null) return false;
            if (targetHealth.IsDead) return false;
            targetHealth.Damage(_weaponComponent.CurrentWeapon);
            ShowBlood(hit);
            PlayKnifeSound(true);
            return true;
        }

        private void PlayKnifeSound(bool hit) =>
            _weaponComponent.CurrentWeapon.PlayAttackSound(hit);

        private void ShowBlood(RaycastHit hit) {
            var position = hit.point;
            var look = Quaternion.LookRotation(- hit.normal);
            if (position != Vector3.zero)
                _bloodPool.Spawn(position, look);
        }

        private void ClearArray() => 
            Array.Clear(_results, 0, _results.Length);

        // #if UNITY_EDITOR
        // private void OnDrawGizmos() {
        //     Gizmos.color = Color.red;
        //     var transform1 = transform;
        //     var pos = transform1.position;
        //     var right = transform1.right;
        //     var startPosition = new Vector3(pos.x, Constants.KnifeHeight + pos.y, pos.z) + right * Constants.StartPointXOffset;
        //     var direction = transform1.forward - right * Constants.DirectionXOffset;
        //     if (_hitDetect) {
        //         Gizmos.DrawRay(startPosition, direction * _hitInfo.distance);
        //         Gizmos.DrawWireSphere(startPosition + direction * _hitInfo.distance, _radius);
        //     } else {
        //         Gizmos.DrawRay(startPosition, direction * _knifeAttackDistance);
        //         Gizmos.DrawWireSphere(startPosition + direction * _knifeAttackDistance, _radius);
        //     }
        // }
        // #endif
    }
}
using System;
using CodeBase.Common;
using CodeBase.Common.Interfaces;
using CodeBase.GraphicEffects;
using CodeBase.Hero.Interfaces;
using CodeBase.Weapon.Interfaces;
using FMODUnity;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;
using Settings = CodeBase.Infrastructure.GameData.Settings;

namespace CodeBase.Attack
{
    public class BulletAttackAbility : MonoBehaviour, IAbility
    {
        private float _shotDistance;
        private float _radius;
        private readonly RaycastHit[] _results = new RaycastHit[50];
        private readonly Vector3[] _points = new Vector3[3];
        private int _layerDefault;
        private int _layerTarget;
        private int _layerDefaultAndTarget;
        private Settings _settings;
        private IWeaponComponent _weaponComponent;
        private Transform _transform;
        private BloodEffect.Pool _bloodPool;
        private BulletHitEffect.Pool _bulletHitPool;
        private float _maxShift;
        private float _accuracy;
        private ICharacter _character;
        private IHasWeapon _hasWeapon;
        private IBulletAttack _bulletAttack;

        [Inject]
        private void Construct(Settings settings, 
                               BloodEffect.Pool bloodPool, 
                               BulletHitEffect.Pool bulletPool) {
            _settings = settings;
            _bloodPool = bloodPool;
            _bulletHitPool = bulletPool;
        }

        private void Awake() {
            _transform = transform;
            _character = GetComponent<ICharacter>();
            _hasWeapon = GetComponent<IHasWeapon>();
            _bulletAttack = GetComponent<IBulletAttack>();
        }

        private void Start() {
            _weaponComponent = _hasWeapon.WeaponComponent;
            _accuracy = _bulletAttack.Accuracy;
            _layerDefault = 1 << LayerMask.NameToLayer(Constants.DefaultLayerName);
            _layerTarget = _character.LayerTarget;
            _layerDefaultAndTarget = _layerDefault | _layerTarget;
            _radius = _settings.ShotRadius;
            _shotDistance = _settings.ShotDistance;
            _maxShift = _settings.MaxShotShiftMagnitude;
        }

        public void Execute() {
            var weapon = _weaponComponent.CurrentWeapon;
            if (NotFirearm(weapon)) return;
            var startPosition = weapon.GunPoint.position;
            var direction = _transform.forward;
            ClearArray();
            GetTargetsInShotDirection(startPosition, direction);
            foreach (var hit in _results) {
                if (hit.collider == null) continue;
                CalculateTargetPointToShot(hit);
                foreach (var point in _points) {
                    RaycastHit hitInfo;
                    if (!HitToTarget(startPosition, point, out hitInfo)) continue;
                    if (NotTarget(hitInfo)) continue;
                    ApplyDamage(hitInfo, weapon);
                    return;
                }
            }
            CheckHitToEnvironment(startPosition, direction);
        }

        private void CheckHitToEnvironment(Vector3 startPosition, Vector3 direction) {
            RaycastHit hitInfo;
            var raycast = Physics.Raycast(
                startPosition, 
                RandomizedDirection(direction), 
                out hitInfo,
                _shotDistance, 
                _layerDefault, 
                QueryTriggerInteraction.Ignore);
            if (raycast)
                ShowBulletHit(hitInfo);
        }

        private static bool NotFirearm(IWeapon weapon) => 
            weapon.WeaponSettings.Type != WeaponType.HandGun && weapon.WeaponSettings.Type != WeaponType.MachineGun;

        private bool NotTarget(RaycastHit hitInfo) => 
            !hitInfo.transform.tag.Equals(_character.EnemyTag);

        private bool HitToTarget(Vector3 startPosition, Vector3 point, out RaycastHit hitInfo) =>
            Physics.Raycast(startPosition, 
                CalculateDirection(startPosition, point), 
                out hitInfo, 
                _shotDistance, 
                _layerDefaultAndTarget, 
                QueryTriggerInteraction.Ignore);

        private Vector3 CalculateDirection(Vector3 startPosition, Vector3 point) {
            var direction = (point - startPosition).normalized;
            return RandomizedDirection(direction);
        }

        private Vector3 RandomizedDirection(Vector3 direction) {
            var randomVector = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            var shift = Vector3.Cross(direction, randomVector).normalized * 
                        (-_maxShift / 100f * _accuracy + _maxShift);
            return (direction + shift).normalized;
        }

        private void CalculateTargetPointToShot(RaycastHit hit) {
            var targetCollider = hit.collider;
            var bounds = targetCollider.bounds;
            var delta = new Vector3(0, bounds.extents.y * 0.75f, 0);
            _points[0] = bounds.center;
            _points[1] = bounds.center + delta;
            _points[2] = bounds.center - delta;
        }

        private void GetTargetsInShotDirection(Vector3 startPosition, Vector3 direction) =>
            Physics.SphereCastNonAlloc(
                startPosition + direction * _radius, 
                _radius, 
                direction, 
                _results,
                _shotDistance - _radius, 
                _layerTarget, 
                QueryTriggerInteraction.Ignore);

        private void ClearArray() => 
            Array.Clear(_results, 0, _results.Length);

        private void ApplyDamage(RaycastHit hitInfo, IWeapon weapon) {
            var targetHealth = hitInfo.transform.GetComponent<IHealth>();
            if (targetHealth == null) return;
            if (targetHealth.IsDead) return;
            targetHealth.Damage(weapon);
            ShowBlood(hitInfo);
        }

        private void ShowBlood(RaycastHit hit) {
            var position = hit.point;
            var look = Quaternion.LookRotation(-hit.normal);
            if (position != Vector3.zero)
                _bloodPool.Spawn(position, look);
        }

        private void ShowBulletHit(RaycastHit raycastHit) {
            var raycastHitPoint = raycastHit.point;
            _bulletHitPool.Spawn(raycastHitPoint, Quaternion.LookRotation(raycastHit.normal));
            RuntimeManager.PlayOneShot(Constants.EventBulletsHit, raycastHitPoint);
        }
    }
}
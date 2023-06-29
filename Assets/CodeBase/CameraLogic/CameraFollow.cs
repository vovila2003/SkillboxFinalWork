using FMODUnity;
using UnityEngine;
using Zenject;
using Settings = CodeBase.Infrastructure.GameData.Settings;

namespace CodeBase.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform Following;
        [SerializeField] private StudioListener StudioListener;

        private Transform _transform;
        private Settings _settings;
        private float _rotationAngleX;
        private float _distanceNear;
        private float _distanceFar;
        private float _offsetY;
        private float _speed;
        private float _excess;
        private float _distance;
        private bool _isFollowingNull;

        [Inject]
        private void Construct(Settings settings) =>
            _settings = settings;
        
        private void Start() {
            _isFollowingNull = true;
            _transform = transform;
            _rotationAngleX = _settings.CameraRotationAngleX;
            _distanceFar = _settings.CameraDistanceFar;
            _distanceNear = _settings.CameraDistanceNear;
            _distance = _distanceNear;
            _offsetY = _settings.CameraOffsetY;
            _speed = _settings.CameraSpeed;
            _excess = _settings.CameraExcess;
        }

        private void LateUpdate() {
            if (_isFollowingNull) return;

            var rotation = Quaternion.Euler(_rotationAngleX, 0, 0);
                
            var position = Vector3.Lerp(_transform.position,
                rotation * new Vector3(0, 0, -_distance) + FollowingPointPosition(), Time.deltaTime * _speed);
            
            _transform.rotation = rotation;
            _transform.position = position;
        }

        public void Follow(GameObject following) {
            Following = following.transform;
            StudioListener.attenuationObject = following;
            _isFollowingNull = false;
        }

        public void ChangeView(bool near) => 
            _distance = near ? _distanceNear : _distanceFar;

        public void DismissCamera() => 
            _isFollowingNull = true;

        private Vector3 FollowingPointPosition() {
            var excess = _excess * _distance / _distanceFar;
            var followingPosition = Following.position + Following.forward * excess;
            followingPosition.y += _offsetY;
            return followingPosition;
        }
    }
}
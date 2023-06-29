using CodeBase.CameraLogic;
using CodeBase.Hero.Abilities;
using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(InteractAbility))]
    public class HeroCamera : MonoBehaviour
    {
        private Camera _camera;
        private CameraFollow _cameraFollow;

        private void Start() {
            _camera = Camera.main;
            if (_camera == null) return;  
            _cameraFollow = _camera.GetComponent<CameraFollow>();
            CameraFollow();
        }

        public void ChangeView(bool near) => 
            _cameraFollow.ChangeView(near);

        public void DismissCamera() =>
            _cameraFollow.DismissCamera();

        private void CameraFollow() => 
            _cameraFollow.Follow(gameObject);
    }
}
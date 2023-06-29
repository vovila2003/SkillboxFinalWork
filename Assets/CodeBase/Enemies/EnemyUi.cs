using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Enemies
{
    public class EnemyUi : MonoBehaviour
    {
        [SerializeField] private Slider HealthSlide;
        [SerializeField] private Slider ArmorSlide;
        private Camera _camera;

        private void Start() => 
            _camera = Camera.main;

        private void LateUpdate() {
            transform.LookAt(_camera.transform);
            transform.Rotate(0, 180, 0);
        }

        public void ChangeHealth(float value) => 
            HealthSlide.value = value;
        
        public void ChangeArmor(float value) => 
            ArmorSlide.value = value;
    }
}
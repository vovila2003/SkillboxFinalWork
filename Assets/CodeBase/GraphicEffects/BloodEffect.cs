using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.GraphicEffects
{
    [RequireComponent(typeof(ParticleSystem), typeof(Transform))]
    public class BloodEffect : MonoBehaviour
    {
        private Pool _pool;
        private ParticleSystem _particleSystem;
        private float _time;
        private Transform _transform;
        
        [Inject]
        private void Construct(Pool pool) => 
            _pool = pool;

        private void Awake() {
            _particleSystem = GetComponent<ParticleSystem>();
            _transform = transform;
            _time = _particleSystem.main.duration;
        }

        private async void DespawnPistolFireEffect() {
            await UniTask.Delay(TimeSpan.FromSeconds(_time));
            _pool.Despawn(this);
        }

        private void Play() {
            _particleSystem.Play();
        }

        private void ResetItem(Vector3 newPosition, Quaternion newRotation) {
            _transform.position = newPosition;
            _transform.rotation = newRotation;
        }

        public class Pool : MonoMemoryPool<Vector3, Quaternion, BloodEffect>
        {
            protected override void Reinitialize(Vector3 position, Quaternion rotation, BloodEffect item) {
                item.ResetItem(position, rotation);
                item.Play();
                item.DespawnPistolFireEffect();
            }
        }
    }
}
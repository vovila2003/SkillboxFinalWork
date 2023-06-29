using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.GraphicEffects
{
    [RequireComponent(typeof(ParticleSystem), typeof(Transform))]
    public class GunFireEffect : MonoBehaviour
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

        private async void DespawnGunFireEffect() {
            await UniTask.Delay(TimeSpan.FromSeconds(_time));
            _transform.parent = null;
            _pool.Despawn(this);
        }

        private void Play() => 
            _particleSystem.Play();

        private void ResetItem(Vector3 newPosition, Quaternion newRotation, Transform parent) {
            _transform.position = newPosition;
            _transform.rotation = newRotation;
            _transform.parent = parent;
        }

        public class Pool : MonoMemoryPool<Transform, GunFireEffect>
        {
            protected override void Reinitialize(Transform parent, GunFireEffect item) {
                item.ResetItem(parent.position, parent.rotation, parent);
                item.Play();
                item.DespawnGunFireEffect();
            }
        }
    }
}
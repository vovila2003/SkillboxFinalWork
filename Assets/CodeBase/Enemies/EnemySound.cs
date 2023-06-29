using System.Collections.Generic;
using FMODUnity;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Enemies
{
    public class EnemySound : MonoBehaviour
    {
        [Required, SerializeField] private List<EventReference> DamageEvents;
        [Required, SerializeField] private EventReference DeathEvent;
        [Required, SerializeField] private StudioEventEmitter Emitter;
        
        private bool _isSpeechPlaying;
        private EventReference _damageEvent;

        private void Awake() => 
            _damageEvent = DamageEvents[Random.Range(0, DamageEvents.Count)];

        [Button]
        public void PlayDamage() => RuntimeManager.PlayOneShot(_damageEvent, transform.position);

        [Button]
        public void PlayDeath() => RuntimeManager.PlayOneShot(DeathEvent, transform.position);
        
        [Button]
        public void PlaySpeech() {
            if (_isSpeechPlaying) return;
            Emitter.Play();
            _isSpeechPlaying = true;
        }

        [Button]
        public void StopSpeech() {
            if (!_isSpeechPlaying) return;
            Emitter.Stop();
            _isSpeechPlaying = false;
        }
    }
}
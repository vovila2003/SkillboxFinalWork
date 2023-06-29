using FMODUnity;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroSound : MonoBehaviour
    {
        [Required, SerializeField] private EventReference DamageEvent;
        [Required, SerializeField] private EventReference DeathEvent;
        [Required, SerializeField] private EventReference LevelUpEvent;
        [Required, SerializeField] private EventReference ApplyArmorEvent;
        [Required, SerializeField] private EventReference ApplyHealthEvent;
        [Required, SerializeField] private EventReference TakeItemEvent;
        

        [Button]
        public void PlayDamage() => RuntimeManager.PlayOneShot(DamageEvent, transform.position);
        
        [Button]
        public void PlayDeath() => RuntimeManager.PlayOneShot(DeathEvent, transform.position);

        [Button]
        public void PlayLevelUp() => RuntimeManager.PlayOneShot(LevelUpEvent, transform.position);
        
        [Button]
        public void PlayApplyArmor() => RuntimeManager.PlayOneShot(ApplyArmorEvent, transform.position);
        
        [Button]
        public void PlayApplyHealth() => RuntimeManager.PlayOneShot(ApplyHealthEvent, transform.position);
        
        [Button]
        public void PlayTakeItem() => RuntimeManager.PlayOneShot(TakeItemEvent, transform.position);
    }
}
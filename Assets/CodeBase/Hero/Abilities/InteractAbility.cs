using CodeBase.Hero.Interfaces;
using ModestTree;
using UnityEngine;

namespace CodeBase.Hero.Abilities
{
    [RequireComponent(typeof(HeroAnimator), typeof(PickUpItemAbility))]
    public class InteractAbility : MonoBehaviour, IAbility
    {
        private IHeroAnimator _heroAnimator;
        private PickUpItemAbility _pickUpItemAbility;
        
        public bool Ready { get; set;}

        private void Awake() {
            _heroAnimator = GetComponent<HeroAnimator>();
            _pickUpItemAbility = GetComponent<PickUpItemAbility>();
        }

        private void Start() => 
            Ready = true;

        public void Execute() {
            if (!Ready || _pickUpItemAbility.Collisions.IsEmpty()) return;
            Ready = false;
            _heroAnimator.Interact();
        }
    }
}
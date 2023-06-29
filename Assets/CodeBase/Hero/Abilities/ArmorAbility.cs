using CodeBase.Hero.Interfaces;
using CodeBase.Infrastructure.GameData;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero.Abilities
{
    [RequireComponent(typeof(HeroInventory))]
    public class ArmorAbility : MonoBehaviour, IAbility
    {
        private HeroInventory _heroInventory;
        private Settings _settings;
        private float _actionTime = float.MinValue;
        private float _actionDelay;

        [Inject]
        private void Construct(Settings settings) => 
            _settings = settings;

        private void Awake() => 
            _heroInventory = GetComponent<HeroInventory>();

        private void Start() => 
            _actionDelay = _settings.HeroActionDelay;

        public void Execute() {
            if (!TimeToDo()) return;
            _heroInventory.ApplyArmorBox();
        }
        
        private bool TimeToDo() {
            var time = Time.time;
            if (time < _actionTime + _actionDelay) return false;
            _actionTime = time;
            return true;
        }
    }
}
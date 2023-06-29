using CodeBase.Hero.Interfaces;
using CodeBase.Infrastructure.GameData;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero.Abilities
{
    public class ChangeWeaponAbility : MonoBehaviour, IPayloadAbility
    {
        public enum ChangeWeaponType
        {
            Next = 0,
            Knife,
            Pistol,
            Gun,
            HideWeapon
        }

        private HeroWeapon _heroWeapon;
        private Settings _settings;
        private float _actionTime = float.MinValue;
        private float _actionDelay;
        private ChangeWeaponType _changeTo;

        public bool Ready { get; set; } = true;

        [Inject]
        private void Construct(Settings settings) => 
            _settings = settings;

        private void Awake() => 
            _heroWeapon = GetComponent<HeroWeapon>();

        private void Start() {
            _actionDelay = _settings.HeroActionDelay;
            _changeTo = ChangeWeaponType.Next;
        }

        public void Execute() {
            if (!_heroWeapon.IsFree || !Ready || !TimeToDo()) return;
            Ready = false;
            _heroWeapon.ChangeWeapon(_changeTo);
        }

        public void ChangeTo(ChangeWeaponType changeWeaponType) => 
            _changeTo = changeWeaponType;

        public void ChangeToKnifeAndExecute() {
            ChangeTo(ChangeWeaponType.Knife);
            Execute();
        }
        
        public void ChangeToPistolAndExecute() {
            ChangeTo(ChangeWeaponType.Pistol);
            Execute();
        }
        
        public void ChangeToGunAndExecute() {
            ChangeTo(ChangeWeaponType.Gun);
            Execute();
        }

        private bool TimeToDo() {
            var time = Time.time;
            if (time < _actionTime + _actionDelay) return false;
            _actionTime = time;
            return true;
        }
    }
}
using CodeBase.Common;
using CodeBase.Hero.Interfaces;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Hero.Abilities
{
    [RequireComponent(typeof(HeroInventory))]
    public class InventoryAbility : MonoBehaviour, IAbility
    {
        private HeroInventory _heroInventory;
        private float _switchTime;
        private InventoryShow _show;
        private Information _information;

        private void Awake() => 
            _heroInventory = GetComponent<HeroInventory>();

        public void Execute() {
            if (!TimeToSwitch()) return;
            if (_heroInventory.HasPack) {
                if (_show == null) return;
                _show.ShowInventory();
            }
            else {
                if (_information == null) return;
                _information.ShowNoPack();
            }
        }

        public void Register(InventoryShow show) => 
            _show = show;

        public void Register(Information info) => 
            _information = info;
        
        private bool TimeToSwitch() {
            var time = Time.time;
            if (time < Constants.SwitchDelay + _switchTime) return false;
            _switchTime = time;
            return true;
        }
    }
}
using CodeBase.Hero.Interfaces;
using UnityEngine;

namespace CodeBase.Hero.LevelUpActions
{
    [RequireComponent(typeof(HeroEquipment))]
    public class LevelUpEquipment : MonoBehaviour, ILevelUp
    {
        private HeroEquipment _heroEquipment;

        private void Awake() => 
            _heroEquipment = GetComponent<HeroEquipment>();

        public void LevelUp(int currentLevel) => 
            _heroEquipment.SetEquipment(currentLevel);
    }
}
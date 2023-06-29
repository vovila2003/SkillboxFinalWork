using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroEquipment : MonoBehaviour
    {
        [SerializeField] private List<GameObject> Bodies;
        [SerializeField] private List<GameObject> Heads;
        [SerializeField] private List<GameObject> Legs;
        [SerializeField] private GameObject Pack;

        public void SetEquipment(int level) {
            if (level < 1 || level > 6) return;
            DisableAll();
            SetBody(level - 1);
            SetHead(level - 1);
            SetLegs((level - 1) / 2);
        }

        [Button]
        private void SetLevel1() => SetEquipment(1);
        
        [Button]
        private void SetLevel2() => SetEquipment(2);
        
        [Button]
        private void SetLevel3() => SetEquipment(3);
        
        [Button]
        private void SetLevel4() => SetEquipment(4);
        
        [Button]
        private void SetLevel5() => SetEquipment(5);
        
        [Button]
        private void SetLevel6() => SetEquipment(6);

        [Button]
        private void SetPackOn() => SetPack(true);

        [Button]
        private void SetPackOff() => SetPack(false);
        
        public void SetPack(bool enable) => 
            Pack.SetActive(enable);

        private void SetLegs(int index) {
            if (Legs.Count > index)
                Legs[index].SetActive(true);
        }

        private void SetBody(int index) {
            if (Bodies.Count > index)
                Bodies[index].SetActive(true);
        }

        private void SetHead(int index) {
            if (Heads.Count > index)
                Heads[index].SetActive(true);
        }

        private void DisableAll() {
            DisableGameObjects(Bodies);
            DisableGameObjects(Heads);
            DisableGameObjects(Legs);
        }

        private void DisableGameObjects(List<GameObject> gameObjects) {
            foreach (GameObject go in gameObjects) {
                go.SetActive(false);
            }
        }
    }
}
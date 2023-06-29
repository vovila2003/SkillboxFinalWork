using CodeBase.Infrastructure.GameData.Interfaces;
using UnityEngine;

namespace CodeBase.Infrastructure.GameData
{
    public class Prefabs : IPrefabs
    {
        public GameObject BackPackPrefab { get; set; }
        public GameObject KnifePrefab { get; set; }
        public GameObject PistolPrefab { get; set; }
        public GameObject MachineGunPrefab { get; set; }
        public GameObject HealthBoxPrefab { get; set; }
        public GameObject ArmorBoxPrefab { get; set; }
        public GameObject BulletsBoxPrefab { get; set; }
        public GameObject PistolFireEffectPrefab { get; set; }
        public GameObject GunFireEffectPrefab { get; set; }
        public GameObject GunFireEnemyOneShotEffectPrefab { get; set; }
        public GameObject BloodEffectPrefab { get; set; }
        public GameObject BulletHitEffectPrefab { get; set; }
        public GameObject HeroPrefab { get; set; }
        public GameObject MeleeLightPrefab { get; set; }
        public GameObject MeleeHeavyPrefab { get; set; }
        public GameObject RangedPrefab { get; set; }
        public GameObject HudPrefab { get; set; }
        public GameObject HudVariablePrefab { get; set; }
        public GameObject InventoryPrefab { get; set; }
        public GameObject StartSceneCanvas { get; set; }
        public GameObject PauseMenu { get; set; }
        public GameObject EnemyUiPrefab { get; set; }
    }
}
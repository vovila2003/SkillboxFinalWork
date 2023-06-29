using UnityEngine;

namespace CodeBase.Infrastructure.GameData.Interfaces
{
    public interface IPrefabs
    {
        GameObject BackPackPrefab { get; }
        GameObject KnifePrefab { get; }
        GameObject PistolPrefab { get; }
        GameObject MachineGunPrefab { get; }
        GameObject HealthBoxPrefab { get; }
        GameObject ArmorBoxPrefab { get; }
        GameObject BulletsBoxPrefab { get; }
        GameObject PistolFireEffectPrefab { get; }
        GameObject GunFireEffectPrefab { get; }
        GameObject GunFireEnemyOneShotEffectPrefab { get; }
        GameObject BloodEffectPrefab { get; }
        GameObject BulletHitEffectPrefab { get; }
        GameObject HeroPrefab { get; }
        GameObject MeleeLightPrefab { get; }
        GameObject MeleeHeavyPrefab { get; }
        GameObject RangedPrefab { get; }
        GameObject HudPrefab { get; }
        GameObject HudVariablePrefab { get; }
        GameObject InventoryPrefab { get; }
        GameObject StartSceneCanvas { get; set; }
        GameObject PauseMenu { get; set; }
        GameObject EnemyUiPrefab { get; }
    }
}
using CodeBase.Infrastructure.GameData;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class PrefabLoader
    {
        public static Prefabs Load(bool isMobile) =>
            new Prefabs {
                ArmorBoxPrefab = LoadPrefab(AssetPaths.ArmorBoxPrefabPath),
                BackPackPrefab = LoadPrefab(AssetPaths.BackPackPrefabPath),
                BulletsBoxPrefab = LoadPrefab(AssetPaths.BulletsBoxPrefabPath),
                MeleeLightPrefab = LoadPrefab(AssetPaths.MeleeLightPrefabPath),
                MeleeHeavyPrefab = LoadPrefab(AssetPaths.MeleeHeavyPrefabPath),
                RangedPrefab = LoadPrefab(AssetPaths.EnemyRangedPrefabPath),
                HealthBoxPrefab = LoadPrefab(AssetPaths.HealthBoxPrefabPath),
                HeroPrefab = LoadPrefab(AssetPaths.HeroPrefabPath),
                HudPrefab = LoadPrefab(isMobile? 
                    AssetPaths.HudPrefabPath : AssetPaths.HudDesktopPrefabPath),
                HudVariablePrefab = LoadPrefab(isMobile? 
                    AssetPaths.HudVariablePrefabPath : AssetPaths.HudVariableDesktopPrefabPath),
                InventoryPrefab = LoadPrefab(AssetPaths.InventoryPrefabPath),
                KnifePrefab = LoadPrefab(AssetPaths.KnifePrefabPath),
                MachineGunPrefab = LoadPrefab(AssetPaths.MachineGunPrefabPath),
                PistolPrefab = LoadPrefab(AssetPaths.PistolPrefabPath),
                PistolFireEffectPrefab = LoadPrefab(AssetPaths.PistolFireEffectPrefabPath),
                GunFireEffectPrefab = LoadPrefab(AssetPaths.GunFireEffectPrefabPath),
                GunFireEnemyOneShotEffectPrefab = LoadPrefab(AssetPaths.GunFireEnemyOneShotEffectPrefabPath),
                BloodEffectPrefab = LoadPrefab(AssetPaths.BloodEffectPrefabPath),
                BulletHitEffectPrefab = LoadPrefab(AssetPaths.BulletHitEffectPrefabPath),
                StartSceneCanvas = LoadPrefab(isMobile? 
                    AssetPaths.StartSceneCanvasMobilePrefabPath : AssetPaths.StartSceneCanvasDesktopPrefabPath),
                PauseMenu = LoadPrefab(isMobile? 
                    AssetPaths.PauseMenuMobilePrefabPath : AssetPaths.PauseMenuDesktopPrefabPath),
                EnemyUiPrefab = LoadPrefab(AssetPaths.EnemyUiPrefabPath),
            };

        private static GameObject LoadPrefab(string path) => 
            Resources.Load<GameObject>(path);
    }
}
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Infrastructure.GameData
{
    [CreateAssetMenu(fileName = "Settings", menuName = "GameSettings", order = 0)]
    public class Settings : ScriptableObject
    {
        [TabGroup("Hero"), LabelWidth(200)] public float HeroRotateSpeed = 5f;
        [TabGroup("Hero"), LabelWidth(200)] public float HeroActionDelay = 0.5f;
        [TabGroup("Hero"), PropertyRange(0, 100)] public float HeroShootAccuracy = 80;
        [TabGroup("Hero"), LabelWidth(200)] public float HeroMenuRotatePeriod = 10f;
        [TabGroup("Hero")] public List<float> HeroMaxHealth;
        [TabGroup("Hero")] public List<float> HeroMaxArmor;
        [TabGroup("Hero")] public List<int> HeroMaxInventoryItems;
        
        [TabGroup("Experience"), LabelWidth(200)] public float HeroExperienceFirstLevelUp = 128f;
        [TabGroup("Experience"), LabelWidth(200)] public float HeroExperienceMultiplier = 1.5f;
        [TabGroup("Experience"), LabelWidth(200)] public float HeroPickUpItemExperience = 5;
        [TabGroup("Experience"), LabelWidth(200)] public float HeroApplyItemExperience = 10;
        [TabGroup("Experience"), LabelWidth(200)] public float HeroShotExperience = 0.1f;
        [TabGroup("Experience"), LabelWidth(200)] public float HeroKillEnemyExperience = 30;
        
        [TabGroup("Weapon"), LabelWidth(200)] public float HeroShootDelay = 0.7f;
        [TabGroup("Weapon"), LabelWidth(200)] public int HeroBulletsPerGunShot = 3;
        [TabGroup("Weapon")] public List<int> HeroMaxPistolBullets;
        [TabGroup("Weapon")] public List<int> HeroMaxGunBullets;

        [TabGroup("Shot"), LabelWidth(200)] public float ShotDistance = 20;
        [TabGroup("Shot"), LabelWidth(200)] public float ShotRadius;
        [TabGroup("Shot"), LabelWidth(200)] public float MaxShotShiftMagnitude = 0.1f;
        
        [TabGroup("Knife"), LabelWidth(200)] public float KnifeAttackDistance;
        [TabGroup("Knife"), LabelWidth(200)] public float KnifeRadius;
        
        [TabGroup("Items"), LabelWidth(200)] public int PistolBulletsInBox = 30;
        [TabGroup("Items"), LabelWidth(200)] public int GunBulletsInBox = 60;
        [TabGroup("Items"), LabelWidth(200)] public int HealthValueInBox = 25;
        [TabGroup("Items"), LabelWidth(200)] public int ArmorValueInBox = 25;

        [TabGroup("Group2", "Camera"), LabelWidth(200)] public float CameraRotationAngleX = 55f;
        [TabGroup("Group2", "Camera"), LabelWidth(200)] public float CameraDistanceFar = 13f;
        [TabGroup("Group2", "Camera"), LabelWidth(200)] public float CameraDistanceNear = 8f;
        [TabGroup("Group2", "Camera"), LabelWidth(200)] public float CameraOffsetY = 0.5f;
        [TabGroup("Group2", "Camera"), LabelWidth(200)] public float CameraSpeed = 2f;
        [TabGroup("Group2", "Camera"), LabelWidth(200)] public float CameraExcess = 2f;
        
        [TabGroup("Group2", "Effects"), LabelWidth(200)] public int PistolFireEffectPoolSize = 1;
        [TabGroup("Group2", "Effects"), LabelWidth(200)] public int GunFireEffectPoolSize = 1;
        [TabGroup("Group2", "Effects"), LabelWidth(200)] public int GunFireEnemyEffectPoolSize = 3;
        [TabGroup("Group2", "Effects"), LabelWidth(200)] public int BloodEffectPoolSize = 3;
        [TabGroup("Group2", "Effects"), LabelWidth(200)] public int BulletHitEffectPoolSize = 3;

        [Space]
        [LabelWidth(200)] public int EnemyDestroyAfterDieDelayMs = 3000;
        [LabelWidth(200)] public float CreateItemAfterEnemyDieThreshold1 = 0.4f;
        [LabelWidth(200)] public float CreateItemAfterEnemyDieThreshold2 = 0.7f;
        
        [TabGroup("Enemies", "MeleeLight"), LabelWidth(200)] public float MeleeLightHealth = 50;
        [TabGroup("Enemies", "MeleeLight"), LabelWidth(200)] public float MeleeLightArmor;
        [TabGroup("Enemies", "MeleeLight"), LabelWidth(200)] public float MeleeLightEvaluateTime;
        [TabGroup("Enemies", "MeleeLight"), LabelWidth(200)] public float MeleeLightAlarmTime;
        [TabGroup("Enemies", "MeleeLight"), LabelWidth(200)] public float MeleeLightAttackDistance;
        [TabGroup("Enemies", "MeleeLight"), LabelWidth(200)] public float MeleeLightPurseFromDistance;
        [TabGroup("Enemies", "MeleeLight"), LabelWidth(200)] public float MeleeLightRotationSpeed;
        [TabGroup("Enemies", "MeleeLight"), LabelWidth(200)] public float MeleeLightShootDelay;
        
        [TabGroup("Enemies", "MeleeHeavy"), LabelWidth(200)] public float MeleeHeavyHealth = 50;
        [TabGroup("Enemies", "MeleeHeavy"), LabelWidth(200)] public float MeleeHeavyArmor = 50;
        [TabGroup("Enemies", "MeleeHeavy"), LabelWidth(200)] public float MeleeHeavyEvaluateTime;
        [TabGroup("Enemies", "MeleeHeavy"), LabelWidth(200)] public float MeleeHeavyAlarmTime;
        [TabGroup("Enemies", "MeleeHeavy"), LabelWidth(200)] public float MeleeHeavyAttackDistance;
        [TabGroup("Enemies", "MeleeHeavy"), LabelWidth(200)] public float MeleeHeavyPurseFromDistance;
        [TabGroup("Enemies", "MeleeHeavy"), LabelWidth(200)] public float MeleeHeavyRotationSpeed;
        [TabGroup("Enemies", "MeleeHeavy"), LabelWidth(200)] public float MeleeHeavyShootDelay;
        
        [TabGroup("Enemies", "Ranged"), LabelWidth(200)] public float RangedHealth = 75;
        [TabGroup("Enemies", "Ranged"), LabelWidth(200)] public float RangedArmor = 75;
        [TabGroup("Enemies", "Ranged"), LabelWidth(200)] public float RangedEvaluateTime;
        [TabGroup("Enemies", "Ranged"), LabelWidth(200)] public float RangedAlarmTime;
        [TabGroup("Enemies", "Ranged"), LabelWidth(200)] public float RangedAttackDistance;
        [TabGroup("Enemies", "Ranged"), LabelWidth(200)] public float RangedPurseFromDistance;
        [TabGroup("Enemies", "Ranged"), LabelWidth(200)] public float RangedRotationSpeed;
        [TabGroup("Enemies", "Ranged"), LabelWidth(200)] public float RangedShootDelay;
        [TabGroup("Enemies", "Ranged"), PropertyRange(0, 100)] public float RangedShootAccuracy;
    }
}
using CodeBase.GraphicEffects;
using CodeBase.Infrastructure.GameData;

namespace CodeBase.Weapon.Interfaces
{
    public interface IFireArms : IWeapon
    {
        bool CanShoot { get; }
        int ReloadTime { get; }
        int ExtraBullets { get; set; }
        int MaxExtraBullets { get; set; }
        bool IsMagazineFull { get; }
        void Reload();
        void PlayReloadSound();
        void PlayEmptySound();
        void SetFullMagazine();
        void RegisterPool(PistolFireEffect.Pool pistolFireEffectPool);
        void RegisterPool(GunFireEffect.Pool gunFireEffectPool, Settings settings);
        void RegisterPool(GunFireEnemyEffect.Pool gunEnemyFireEffectPool);
    }
}
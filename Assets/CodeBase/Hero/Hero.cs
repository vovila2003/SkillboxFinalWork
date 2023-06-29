using CodeBase.Common;
using CodeBase.Common.Interfaces;
using CodeBase.Hero.ComponentData;
using Unity.Entities;
using UnityEngine;
using Zenject;
using Settings = CodeBase.Infrastructure.GameData.Settings;

namespace CodeBase.Hero
{
    public class Hero : MonoBehaviour, IConvertGameObjectToEntity, ICharacter, IBulletAttack, IHasWeapon
    {
        private IWeaponComponent _weaponComponent;
        private Settings _settings;

        public float Accuracy => _settings.HeroShootAccuracy;
        public int LayerTarget => 1 << LayerMask.NameToLayer(Constants.EnemyLayerName);
        public string EnemyTag => Constants.EnemyTag;
        public IWeaponComponent WeaponComponent => _weaponComponent;

        [Inject]
        private void Construct(Settings settings) => 
            _settings = settings;

        private void Awake() => 
            _weaponComponent = GetComponent<IWeaponComponent>();

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) => 
            dstManager.AddComponentData(entity, new HeroTagData());
    }
}
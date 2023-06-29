using CodeBase.Hero.ComponentData;
using CodeBase.Hero.Interfaces;
using CodeBase.Infrastructure.GameData;
using Sirenix.OdinInspector;
using Unity.Entities;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero
{
    public class UserInput : MonoBehaviour, IConvertGameObjectToEntity
    {
        [TabGroup("Actions")] [Required] [SerializeField] private MonoBehaviour ShootAction;
        [TabGroup("Actions")] [Required] [SerializeField] private MonoBehaviour ChangeWeaponAction;
        [TabGroup("Actions")] [Required] [SerializeField] private MonoBehaviour ReloadAction;
        [TabGroup("Actions")] [Required] [SerializeField] private MonoBehaviour InventoryAction;
        [TabGroup("Actions")] [Required] [SerializeField] private MonoBehaviour InteractAction;
        [TabGroup("Actions")] [Required] [SerializeField] private MonoBehaviour ApplyArmorAction;
        [TabGroup("Actions")] [Required] [SerializeField] private MonoBehaviour HealAction;
        
        private Settings _settings;
        
        public MonoBehaviour ShootAbility => ShootAction;
        public MonoBehaviour ChangeWeaponAbility => ChangeWeaponAction;
        public MonoBehaviour ReloadAbility => ReloadAction;
        public MonoBehaviour InventoryAbility => InventoryAction;
        public MonoBehaviour InteractAbility => InteractAction;
        public MonoBehaviour ArmorAbility => ApplyArmorAction;
        public MonoBehaviour HealAbility => HealAction;

        [Inject]
        private void Construct(Settings settings) =>
            _settings = settings;
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
            dstManager.AddComponentData(entity, new InputData());
            dstManager.AddComponentData(entity, new MoveData {
                RotateSpeed = _settings.HeroRotateSpeed
            });
            
            if (ShootAction != null && ShootAction is IAbility)
                dstManager.AddComponentData(entity, new ShootData());
        }
    }
}
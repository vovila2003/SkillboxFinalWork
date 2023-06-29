using CodeBase.Hero;
using CodeBase.Hero.Abilities;
using CodeBase.Infrastructure.Factories.Interfaces;
using CodeBase.Infrastructure.GameData.Interfaces;
using CodeBase.UI;
using CodeBase.UI.Interfaces;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Factories
{
    public class HeroFactory : IHeroFactory
    {
        private readonly DiContainer _diContainer;
        private readonly IPrefabs _prefabs;
        private GameObject _hero;
        private HeroArmor _heroArmor;
        private HeroHealth _heroHealth;
        private HeroWeapon _heroWeapon;
        private InventoryAbility _inventoryAbility;

        public HeroFactory(DiContainer diContainer, IPrefabs prefabs) {
            _diContainer = diContainer;
            _prefabs = prefabs;
        }

        public Transform Create(Vector3 at) {
            var prefab = _prefabs.HeroPrefab;
            _hero = Object.Instantiate(_prefabs.HeroPrefab, at, prefab.transform.rotation);
            SetupHero();
            return _hero.GetComponent<Transform>();
        }

        public void DestroyHero() {
            _hero.GetComponent<HeroCamera>().DismissCamera();
            Object.Destroy(_hero);
        }

        private void SetupHero() {
            _diContainer.Inject(_hero);
            CreateHud();
            CreateInventory();
            SetupHudVariable();
        }

        private void CreateHud() {
            var hud = Object.Instantiate(_prefabs.HudPrefab);
            _diContainer.Inject(hud);
        }

        private void CreateInventory() {
            var inventory = Object.Instantiate(_prefabs.InventoryPrefab);
            SetupInventory(inventory);
        }

        private void SetupInventory(GameObject inventory) {
            _diContainer.Inject(inventory);
            inventory.GetComponent<Canvas>().enabled = false;
            RegisterInventoryButtons(inventory);
            RegisterInventoryShow(inventory);
            RegisterInventoryViewModel(inventory);
        }

        private void RegisterInventoryButtons(GameObject inventory) {
            RegisterButtons<HealthApplyButton>(inventory);
            RegisterButtons<ArmorApplyButton>(inventory);
            RegisterButtons<KnifeButton>(inventory);
            RegisterButtons<PistolButton>(inventory);
            RegisterButtons<GunButton>(inventory);
        }

        private void RegisterInventoryShow(GameObject inventory) {
            _inventoryAbility = _hero.GetComponent<InventoryAbility>();
            _inventoryAbility.Register(inventory.GetComponent<InventoryShow>());
        }

        private void RegisterInventoryViewModel(GameObject inventory) {
            var inventoryModel = inventory.GetComponent<UiViewModel>();
            _heroArmor = _hero.GetComponent<HeroArmor>();
            _heroArmor.Register(inventoryModel);
            _heroHealth = _hero.GetComponent<HeroHealth>();
            _heroHealth.RegisterModel(inventoryModel);
            _hero.GetComponent<HeroInventory>().Register(inventoryModel);
            _heroWeapon = _hero.GetComponent<HeroWeapon>();
            _heroWeapon.RegisterModel(inventoryModel);
        }

        private void RegisterButtons<TButton>(GameObject inventory) where TButton : IRegisterHero { 
            var applyButton = inventory.GetComponentInChildren<TButton>();
            if (applyButton != null)
                applyButton.RegisterHero(_hero);
            else
                Debug.Log("ApplyButton is null");
        }

        private void SetupHudVariable() {
            var hudVariable = Object.Instantiate(_prefabs.HudVariablePrefab);
            RegisterHudViewModel(hudVariable);
            RegisterHudInformation(hudVariable);
        }

        private void RegisterHudViewModel(GameObject hudVariable) {
            var hudViewModel = hudVariable.GetComponent<HudViewModel>();
            _heroArmor.Register(hudViewModel);
            _heroHealth.RegisterModel(hudViewModel);
            _heroWeapon.RegisterModel(hudViewModel);
            _hero.GetComponent<HeroExperienceLevel>().RegisterModel(hudViewModel);
        }

        private void RegisterHudInformation(GameObject hudVariable) {
            var info = hudVariable.GetComponent<Information>();
            _inventoryAbility.Register(info);
            _hero.GetComponent<PickUpItemAbility>().Register(info);
        }
    }
}
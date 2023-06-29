using CodeBase.Hero.ComponentData;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeBase.Hero.Systems 
{
    public partial class UserInputSystem : SystemBase 
    {
        private InputAction _moveAction;
        private InputAction _shootAction;
        private InputAction _changeWeaponAction; 
        private InputAction _reloadAction; 
        private InputAction _inventoryAction;
        private InputAction _interactAction;
        private InputAction _armorAction;
        private InputAction _healAction;
        private InputAction _hideWeaponAction;
        private InputAction _knifeAction;
        private InputAction _pistolAction;
        private InputAction _gunAction;
        private InputAction _exitAction;
        private float2 _moveInput;
        private float _shootInput;
        private float _changeWeaponInput;
        private float _reloadInput;
        private float _inventoryInput;
        private float _interactInput;
        private float _armorInput;
        private float _healInput;
        private float _knifeInput;
        private float _pistolInput;
        private float _gunInput;
        private float _hideWeaponInput;
        private float _exitInput;

        protected override void OnStartRunning() {
            _moveAction = new InputAction("move", binding: "<Gamepad>/leftStick");
            _moveAction.AddCompositeBinding("Dpad")
                .With("Up", "<Keyboard>/w")
                .With("Down", "<Keyboard>/s")
                .With("Left", "<Keyboard>/a")
                .With("Right", "<Keyboard>/d");

            _moveAction.performed += context => { _moveInput = context.ReadValue<Vector2>(); };
            _moveAction.started += context => { _moveInput = context.ReadValue<Vector2>(); };
            _moveAction.canceled += context => { _moveInput = 0; };
            _moveAction.Enable();

            _shootAction = new InputAction("shoot", binding: "<Gamepad>/ButtonNorth");
            _shootAction.AddBinding("<Keyboard>/Space");
            _shootAction.performed += context => { _shootInput = context.ReadValue<float>(); };
            _shootAction.started += context => { _shootInput = context.ReadValue<float>(); };
            _shootAction.canceled += context => { _shootInput = 0; };
            _shootAction.Enable();
            
            _changeWeaponAction = new InputAction("changeWeapon", binding: "<Gamepad>/ButtonEast");
            _changeWeaponAction.AddBinding("<Keyboard>/Tab");
            _changeWeaponAction.performed += context => { _changeWeaponInput = context.ReadValue<float>(); };
            _changeWeaponAction.started += context => { _changeWeaponInput = context.ReadValue<float>(); };
            _changeWeaponAction.canceled += context => { _changeWeaponInput = 0; };
            _changeWeaponAction.Enable();

            _reloadAction = new InputAction("reload", binding: "<Gamepad>/ButtonWest");
            _reloadAction.AddBinding("<Keyboard>/r");
            _reloadAction.performed += context => { _reloadInput = context.ReadValue<float>(); };
            _reloadAction.started += context => { _reloadInput = context.ReadValue<float>(); };
            _reloadAction.canceled += context => { _reloadInput = 0; };
            _reloadAction.Enable();
            
            _inventoryAction = new InputAction("inventory", binding: "<Gamepad>/RightTrigger");
            _inventoryAction.AddBinding("<Keyboard>/q");
            _inventoryAction.performed += context => { _inventoryInput = context.ReadValue<float>(); };
            _inventoryAction.started += context => { _inventoryInput = context.ReadValue<float>(); };
            _inventoryAction.canceled += context => { _inventoryInput = 0; };
            _inventoryAction.Enable();
            
            _interactAction = new InputAction("interact", binding: "<Gamepad>/ButtonSouth");
            _interactAction.AddBinding("<Keyboard>/e");
            _interactAction.performed += context => { _interactInput = context.ReadValue<float>(); };
            _interactAction.started += context => { _interactInput = context.ReadValue<float>(); };
            _interactAction.canceled += context => { _interactInput = 0; };
            _interactAction.Enable();
            
            _armorAction = new InputAction("armor", binding: "<Keyboard>/b");
            _armorAction.performed += context => { _armorInput = context.ReadValue<float>(); };
            _armorAction.started += context => { _armorInput = context.ReadValue<float>(); };
            _armorAction.canceled += context => { _armorInput = 0; };
            _armorAction.Enable();

            _healAction = new InputAction("heal", binding: "<Keyboard>/h");
            _healAction.performed += context => { _healInput = context.ReadValue<float>(); };
            _healAction.started += context => { _healInput = context.ReadValue<float>(); };
            _healAction.canceled += context => { _healInput = 0; };
            _healAction.Enable();
            
            _hideWeaponAction = new InputAction("hideWeapon", binding: "<Keyboard>/t");
            _hideWeaponAction.performed += context => { _hideWeaponInput = context.ReadValue<float>(); };
            _hideWeaponAction.started += context => { _hideWeaponInput = context.ReadValue<float>(); };
            _hideWeaponAction.canceled += context => { _hideWeaponInput = 0; };
            _hideWeaponAction.Enable();
            
            _knifeAction = new InputAction("knife", binding: "<Keyboard>/1");
            _knifeAction.performed += context => { _knifeInput = context.ReadValue<float>(); };
            _knifeAction.started += context => { _knifeInput = context.ReadValue<float>(); };
            _knifeAction.canceled += context => { _knifeInput = 0; };
            _knifeAction.Enable();
            
            _pistolAction = new InputAction("pistol", binding: "<Keyboard>/2");
            _pistolAction.performed += context => { _pistolInput = context.ReadValue<float>(); };
            _pistolAction.started += context => { _pistolInput = context.ReadValue<float>(); };
            _pistolAction.canceled += context => { _pistolInput = 0; };
            _pistolAction.Enable();
            
            _gunAction = new InputAction("gun", binding: "<Keyboard>/3");
            _gunAction.performed += context => { _gunInput = context.ReadValue<float>(); };
            _gunAction.started += context => { _gunInput = context.ReadValue<float>(); };
            _gunAction.canceled += context => { _gunInput = 0; };
            _gunAction.Enable();
            
            _exitAction = new InputAction("exit", binding: "<Gamepad>/Start");
            _exitAction.AddBinding("<Keyboard>/Escape");
            _exitAction.performed += context => { _exitInput = context.ReadValue<float>(); };
            _exitAction.started += context => { _exitInput = context.ReadValue<float>(); };
            _exitAction.canceled += context => { _exitInput = 0; };
            _exitAction.Enable();
        }

        protected override void OnStopRunning() {
            _moveAction.Disable();
            _shootAction.Disable();
            _changeWeaponAction.Disable();
            _reloadAction.Disable();
            _inventoryAction.Disable();
            _interactAction.Disable();
            _armorAction.Disable();
            _healAction.Disable();
            _hideWeaponAction.Disable();
            _knifeAction.Disable();
            _pistolAction.Disable();
            _gunAction.Disable();
            _exitAction.Disable();
        }

        protected override void OnUpdate() {
            Entities
                .ForEach(
                    (HeroHealth health, ref InputData inputData) => {
                        if (health.IsDead) return;
                        inputData.Move = _moveInput;
                        inputData.Shoot = _shootInput;
                        inputData.ChangeWeapon = _changeWeaponInput;
                        inputData.Reload = _reloadInput;
                        inputData.Inventory = _inventoryInput;
                        inputData.Interact = _interactInput;
                        inputData.Armor = _armorInput;
                        inputData.Heal = _healInput;
                        inputData.NoWeapon = _hideWeaponInput;
                        inputData.Knife = _knifeInput;
                        inputData.Pistol = _pistolInput;
                        inputData.Gun = _gunInput;
                        inputData.Exit = _exitInput;
                    }
                )
                .WithoutBurst()
                .Run();
        }
    }
}
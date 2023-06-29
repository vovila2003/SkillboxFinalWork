using CodeBase.Common;
using CodeBase.Hero.Abilities;
using CodeBase.Hero.ComponentData;
using CodeBase.Hero.Interfaces;
using Unity.Entities;
using UnityEngine;

namespace CodeBase.Hero.Systems
{
    public partial class CharacterAbilitiesSystem : SystemBase
    {
        protected override void OnUpdate() {
            Entities
                .WithChangeFilter<InputData>()
                .ForEach(
                    (HeroHealth health, ref InputData inputData, in UserInput userInput) => {
                        if (health.IsDead) return;
                        
                        ExecuteAbility(inputData.Shoot, userInput.ShootAbility);

                        ExecuteAbility(inputData.Reload, userInput.ReloadAbility);

                        ExecuteAbility(inputData.Inventory, userInput.InventoryAbility);

                        ExecuteAbility(inputData.Interact, userInput.InteractAbility);

                        ExecuteAbility(inputData.Armor, userInput.ArmorAbility);

                        ExecuteAbility(inputData.Heal, userInput.HealAbility);
                        
                        ExecuteChangeWeaponAbility(
                            inputData.ChangeWeapon,
                            inputData.NoWeapon,
                            inputData.Knife,
                            inputData.Pistol,
                            inputData.Gun,
                            userInput.ChangeWeaponAbility);
                    }
                )
                .WithoutBurst()
                .Run();
        }

        private static void ExecuteChangeWeaponAbility(float changeWeapon, float noWeapon, 
                                                       float knife, float pistol, 
                                                       float gun, Object ability) {
            if (ability == null || ability is not IPayloadAbility iAbility) return;
            if (changeWeapon > Constants.Threshold) {
                iAbility.ChangeTo(ChangeWeaponAbility.ChangeWeaponType.Next);
                iAbility.Execute();
            } else if (noWeapon > Constants.Threshold) {
                iAbility.ChangeTo(ChangeWeaponAbility.ChangeWeaponType.HideWeapon);
                iAbility.Execute();
            } else if (knife > Constants.Threshold) {
                iAbility.ChangeTo(ChangeWeaponAbility.ChangeWeaponType.Knife);
                iAbility.Execute();
            } else if (pistol > Constants.Threshold) {
                iAbility.ChangeTo(ChangeWeaponAbility.ChangeWeaponType.Pistol);
                iAbility.Execute();
            } else if (gun > Constants.Threshold) {
                iAbility.ChangeTo(ChangeWeaponAbility.ChangeWeaponType.Gun);
                iAbility.Execute();
            }
        }

        private static void ExecuteAbility(float input, Object ability) {
            if (input > Constants.Threshold && ability != null && ability is IAbility iAbility)
                iAbility.Execute();
        }
    }
}
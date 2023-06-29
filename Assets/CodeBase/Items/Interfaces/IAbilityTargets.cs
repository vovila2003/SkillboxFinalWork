using System.Collections.Generic;
using CodeBase.Hero.Interfaces;
using UnityEngine;

namespace CodeBase.Items.Interfaces
{
    public interface IAbilityTargets : IAbility
    {
        List<GameObject> Targets { get; set; }
    }
}
using CodeBase.Common;
using UnityEngine;

namespace CodeBase.Weapon
{
    [CreateAssetMenu(fileName = "WeaponSettings", menuName = "WeaponSettings", order = 0)]
    public class WeaponSettingsSo : ScriptableObject
    {
        public WeaponType Type;
        public float Damage;
        public float ArmorPenetration;
        public int BulletsInMagazine;
        public bool IsNotEmpty;
    }
}
using UnityEngine;

namespace CodeBase.Hero
{
    [CreateAssetMenu(fileName = "HeroLevel", menuName = "Hero/HeroLevel", order = 0)]
    public class HeroLevel : ScriptableObject
    {
        public int Level;
    }
}
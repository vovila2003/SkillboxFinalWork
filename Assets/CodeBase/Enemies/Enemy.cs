using CodeBase.Common;
using CodeBase.Common.Interfaces;
using UnityEngine;

namespace CodeBase.Enemies
{
    public class Enemy : MonoBehaviour, ICharacter
    {
        private Transform _heroTransform;

        public string EnemyTag => Constants.HeroTag;
        public int LayerTarget => 1 << LayerMask.NameToLayer(Constants.HeroLayerName);

        public Transform Hero {
            get => _heroTransform ;
            set {
                if (value == null) return;
                _heroTransform = value;
            }
        }
    }
}
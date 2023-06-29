using CodeBase.Common;
using CodeBase.Enemies.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Enemies
{
    public class EnemyArmor : MonoBehaviour
    {
        private float _armor;
        private IEnemyType _enemyType;
        
        [ShowInInspector]
        public float Current {
            get => _armor;
            set {
                if (Mathf.Abs(_armor - value) < Constants.Threshold) return;
                _armor = value;
                if (_armor < 0) _armor = 0;
            }
        }

        private void Awake() => 
            _enemyType = GetComponent<IEnemyType>();

        private void Start() => 
            Current = _enemyType.Armor;
    }
}
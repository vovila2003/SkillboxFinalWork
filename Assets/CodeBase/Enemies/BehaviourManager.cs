using System.Collections.Generic;
using CodeBase.Enemies.ComponentData;
using CodeBase.Enemies.Interfaces;
using Sirenix.OdinInspector;
using Unity.Entities;
using UnityEngine;

namespace CodeBase.Enemies
{
    public class BehaviourManager : MonoBehaviour, IConvertGameObjectToEntity
    {
        [SerializeField] private List<MonoBehaviour> Behaviours = new List<MonoBehaviour>();

        private float _evaluateTime;
        private IEnemyType _enemyType;

        public List<IBehaviour> CheckedBehaviours { get; } = new List<IBehaviour>();
        
        [ShowInInspector] public IBehaviour ActiveBehaviour { get; set; }

        private void Start() {
            _enemyType = GetComponent<IEnemyType>();
            _evaluateTime = _enemyType.EvaluateTime;
            foreach (var behaviour in Behaviours) {
                if (behaviour is IBehaviour iBehaviour)
                    CheckedBehaviours.Add(iBehaviour);
                else {
                    Debug.Log("Behaviours must derive from IBehaviour");
                }
            }
        }

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
            dstManager.AddComponentData(entity, new AIAgentData {
                EvaluateTime = _evaluateTime,
                CurrentTime = _evaluateTime
            });
        }
    }
}
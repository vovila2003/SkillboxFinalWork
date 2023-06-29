using CodeBase.Hero;
using CodeBase.Infrastructure.StateMachine;
using CodeBase.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class Bootstrapper : MonoBehaviour
    {
        public static Bootstrapper Instance;

        [Required, SerializeField] private LoadingCurtain LoadingCurtain;
        private HeroLevel _heroLevel;

        public Game Game { get; private set; }


        [ShowInInspector] private string CurrentState => Game?.StateMachine?.ActiveState;

        [Inject]
        private void Construct(HeroLevel heroLevel) => 
            _heroLevel = heroLevel;

        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            
            Game = new Game(LoadingCurtain, _heroLevel);
            Game.StateMachine.Enter<BootstrapperState>();
            
            DontDestroyOnLoad(this);
        }
    }
}
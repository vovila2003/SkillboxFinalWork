using CodeBase.Hero;
using CodeBase.Infrastructure.StateMachine.Interfaces;
using CodeBase.UI;

namespace CodeBase.Infrastructure.StateMachine
{
    public class BootstrapperState : IState
    {
        private const int HeroStartLevel = 1;
        private const string StartScene = "StartScene";
        
        private readonly Game _game;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly HeroLevel _heroLevel;
        
        public BootstrapperState(Game game, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, HeroLevel heroLevel) {
            _game = game;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _heroLevel = heroLevel;
        }

        public void Enter() {
            _loadingCurtain.Show();
#if UNITY_ANDROID
            _game.Prefabs = PrefabLoader.Load(isMobile: true);
#else
            _game.Prefabs = PrefabLoader.Load(isMobile: false);
#endif            
            _heroLevel.Level = HeroStartLevel;
            _sceneLoader.Load(StartScene, OnLoadedStartScene);
        }

        private void OnLoadedStartScene() {
            _loadingCurtain.Hide();
        }

        public void Exit() { }
    }
}
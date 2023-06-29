using CodeBase.Infrastructure.StateMachine.Interfaces;
using CodeBase.UI;

namespace CodeBase.Infrastructure.StateMachine
{
    public class EndGameState : IState
    {
        private const string StartLevel = "StartScene";
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;

        public EndGameState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain) {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter() {
            _loadingCurtain.Show();
            _sceneLoader.Load(StartLevel, OnLoaded);
        }

        public void Exit() { }

        private void OnLoaded() => 
            _loadingCurtain.Hide();
    }
}
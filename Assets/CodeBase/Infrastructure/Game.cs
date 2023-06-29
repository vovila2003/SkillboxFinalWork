using CodeBase.Common;
using CodeBase.Hero;
using CodeBase.Infrastructure.GameData;
using CodeBase.Infrastructure.StateMachine;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        public Prefabs Prefabs { get; set; }
        public GameStateMachine StateMachine { get; }
        
        public PauseMenu Pause { get; set; }
        public ResultScreen Result { get; set; }

        public Game(LoadingCurtain loadingCurtain, HeroLevel heroLevel) => 
            StateMachine = new GameStateMachine(this, new SceneLoader(loadingCurtain), loadingCurtain, heroLevel);

        public void Play() => 
            StateMachine.Enter<LoadLevelState, string>(Constants.FirstLevel);

        public void ToMainMenu() {
            Time.timeScale = 1;
            StateMachine.Enter<EndGameState>();
        }
    }
}
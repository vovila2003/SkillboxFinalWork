using CodeBase.Infrastructure.StateMachine.Interfaces;

namespace CodeBase.Infrastructure.StateMachine
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        public GameLoopState(GameStateMachine gameStateMachine) {
            _gameStateMachine = gameStateMachine;
        }

        public void Exit() { }

        public void Enter() { }
    }
}
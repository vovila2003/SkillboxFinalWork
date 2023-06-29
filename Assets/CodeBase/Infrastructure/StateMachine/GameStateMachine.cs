using System;
using System.Collections.Generic;
using CodeBase.Hero;
using CodeBase.Infrastructure.StateMachine.Interfaces;
using CodeBase.UI;

namespace CodeBase.Infrastructure.StateMachine
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public string ActiveState =>
                _activeState switch {
                    BootstrapperState _ => "BootstrapperState",
                    LoadLevelState _ => "LoadLevelState",
                    GameLoopState _ => "GameLoopState",
                    EndGameState _ => "EndGameState",
                    _ => "Unknown State"
                };

        public GameStateMachine(Game game, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, HeroLevel heroLevel) {
            _states = new Dictionary<Type, IExitableState> {
                [typeof(BootstrapperState)] = new BootstrapperState(game, sceneLoader, loadingCurtain, heroLevel),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingCurtain),
                [typeof(GameLoopState)] = new GameLoopState(this),
                [typeof(EndGameState)] = new EndGameState(this, sceneLoader, loadingCurtain)
            };
        }

        public void Enter<TState>() where TState : class, IState {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload> {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState => 
            _states[typeof(TState)] as TState;
    }
}
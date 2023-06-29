namespace CodeBase.Infrastructure.StateMachine.Interfaces
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}
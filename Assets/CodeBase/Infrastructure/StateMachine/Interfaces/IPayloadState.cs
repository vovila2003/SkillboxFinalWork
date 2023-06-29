namespace CodeBase.Infrastructure.StateMachine.Interfaces
{
    public interface IPayloadState<TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
}
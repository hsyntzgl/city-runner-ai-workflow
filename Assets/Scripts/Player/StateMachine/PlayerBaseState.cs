public abstract class PlayerBaseState : IPlayerState
{
    protected readonly PlayerController Controller;
    protected readonly PlayerStateMachine StateMachine;

    protected PlayerBaseState(PlayerController controller, PlayerStateMachine stateMachine)
    {
        Controller = controller;
        StateMachine = stateMachine;
    }

    public virtual void Enter() { }
    public virtual void Tick() { }
    public virtual void Exit() { }
}

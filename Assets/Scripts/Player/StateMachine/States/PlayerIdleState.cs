public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerController controller, PlayerStateMachine stateMachine) : base(controller, stateMachine) { }

    public override void Tick()
    {
        Controller.ApplyGravity();
        Controller.Move(UnityEngine.Vector3.zero);

        if (Controller.JumpPressedThisFrame && Controller.IsGrounded)
        {
            StateMachine.ChangeState(Controller.JumpState);
            return;
        }

        if (Controller.MoveInput.sqrMagnitude > 0.01f)
        {
            StateMachine.ChangeState(Controller.MoveState);
        }
    }
}

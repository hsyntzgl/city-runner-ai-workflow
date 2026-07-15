using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerController controller, PlayerStateMachine stateMachine) : base(controller, stateMachine) { }

    public override void Enter()
    {
        Controller.StartJump();
    }

    public override void Tick()
    {
        Controller.ApplyGravity();

        Vector3 direction = Controller.InputToWorldDirection();
        Controller.Move(direction);

        if (Controller.IsGrounded)
        {
            StateMachine.ChangeState(direction.sqrMagnitude > 0.01f ? Controller.MoveState : Controller.IdleState);
        }
    }
}

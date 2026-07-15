using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public PlayerMoveState(PlayerController controller, PlayerStateMachine stateMachine) : base(controller, stateMachine) { }

    public override void Tick()
    {
        Controller.ApplyGravity();

        Vector3 direction = Controller.InputToWorldDirection();
        Controller.Move(direction);

        if (Controller.JumpPressedThisFrame && Controller.IsGrounded)
        {
            StateMachine.ChangeState(Controller.JumpState);
            return;
        }

        if (direction.sqrMagnitude <= 0.01f)
        {
            StateMachine.ChangeState(Controller.IdleState);
        }
    }
}

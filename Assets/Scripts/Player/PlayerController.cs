using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionAsset inputActions;

    [Header("Camera")]
    [SerializeField] private Transform cameraTransform;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravity = -9.81f;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction sprintAction;
    private PlayerStateMachine stateMachine;
    private PlayerStamina stamina;
    private float verticalVelocity;

    public CharacterController Controller { get; private set; }
    public Vector2 MoveInput => moveAction.ReadValue<Vector2>();
    public bool JumpPressedThisFrame => jumpAction.WasPerformedThisFrame();
    public bool IsGrounded => Controller.isGrounded;
    public bool IsSprinting => sprintAction.IsPressed() && (stamina == null || stamina.CanSprint);
    public float MoveSpeed => moveSpeed;
    public float SprintSpeed => sprintSpeed;

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }

    private void Awake()
    {
        Controller = GetComponent<CharacterController>();
        stamina = GetComponent<PlayerStamina>();

        InputActionMap playerMap = inputActions.FindActionMap("Player");
        moveAction = playerMap.FindAction("Move");
        jumpAction = playerMap.FindAction("Jump");
        sprintAction = playerMap.FindAction("Sprint");

        stateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, stateMachine);
        MoveState = new PlayerMoveState(this, stateMachine);
        JumpState = new PlayerJumpState(this, stateMachine);
    }

    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
        sprintAction.Enable();
        stateMachine.Initialize(IdleState);
    }

    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
        sprintAction.Disable();
    }

    private void Update()
    {
        stateMachine.Tick();
    }

    public Vector3 InputToWorldDirection()
    {
        Vector2 input = MoveInput;

        if (cameraTransform == null)
        {
            return new Vector3(input.x, 0f, input.y);
        }

        Vector3 forward = cameraTransform.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 right = cameraTransform.right;
        right.y = 0f;
        right.Normalize();

        return forward * input.y + right * input.x;
    }

    public void ApplyGravity()
    {
        if (IsGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -2f;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
    }

    public void StartJump()
    {
        verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    public void Move(Vector3 horizontalDirection)
    {
        float currentSpeed = IsSprinting ? sprintSpeed : moveSpeed;
        Vector3 motion = horizontalDirection.normalized * currentSpeed;
        motion.y = verticalVelocity;
        Controller.Move(motion * Time.deltaTime);

        if (horizontalDirection.sqrMagnitude > 0.0001f)
        {
            Vector3 direction = horizontalDirection.normalized;
            float facingDot = Vector3.Dot(direction, transform.forward);

            // Only turn to face the movement direction when it's roughly forward-ish
            // relative to the current facing. A sharp reversal (e.g. pure backward
            // input) is treated as walking backward instead of spinning around.
            if (facingDot > -0.5f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}

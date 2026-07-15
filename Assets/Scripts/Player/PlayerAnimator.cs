using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerController))]
public class PlayerAnimator : MonoBehaviour
{
    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int[] IdleVariantHashes =
    {
        Animator.StringToHash("PlayBreathingIdle"),
        Animator.StringToHash("PlayHappyIdle"),
        Animator.StringToHash("PlaySadIdle"),
    };

    [SerializeField] private float dampTime = 0.1f;

    [Header("Idle Variations")]
    [SerializeField] private float minIdleVariantDelay = 4f;
    [SerializeField] private float maxIdleVariantDelay = 9f;

    private Animator animator;
    private PlayerController controller;
    private float idleTimer;
    private float nextIdleVariantDelay;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
        nextIdleVariantDelay = Random.Range(minIdleVariantDelay, maxIdleVariantDelay);
    }

    private void Update()
    {
        Vector3 direction = controller.InputToWorldDirection();
        float speed = 0f;

        if (direction.sqrMagnitude > 0.0001f)
        {
            float magnitude = controller.IsSprinting ? controller.SprintSpeed : controller.MoveSpeed;
            speed = Vector3.Dot(direction.normalized, transform.forward) * magnitude;
        }

        animator.SetFloat(SpeedHash, speed, dampTime, Time.deltaTime);

        UpdateIdleVariants();
    }

    private void UpdateIdleVariants()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            idleTimer = 0f;
            return;
        }

        idleTimer += Time.deltaTime;
        if (idleTimer < nextIdleVariantDelay)
        {
            return;
        }

        idleTimer = 0f;
        nextIdleVariantDelay = Random.Range(minIdleVariantDelay, maxIdleVariantDelay);
        animator.SetTrigger(IdleVariantHashes[Random.Range(0, IdleVariantHashes.Length)]);
    }
}

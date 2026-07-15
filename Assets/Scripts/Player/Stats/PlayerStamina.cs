using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerStamina : StatSystem
{
    [SerializeField] private float drainPerSecond = 20f;
    [SerializeField] private float regenPerSecond = 15f;
    [SerializeField] private float regenDelay = 1f;
    [SerializeField, Range(0f, 1f)] private float exhaustedRecoveryFraction = 0.2f;

    private PlayerController controller;
    private bool isExhausted;
    private float regenDelayTimer;

    public bool CanSprint => !isExhausted && currentValue > 0f;

    protected override void Awake()
    {
        base.Awake();
        controller = GetComponent<PlayerController>();
    }

    private void Update()
    {
        Vector3 direction = controller.InputToWorldDirection();
        bool wantsSprint = controller.IsSprinting && direction.sqrMagnitude > 0.0001f;

        if (wantsSprint)
        {
            Modify(-drainPerSecond * Time.deltaTime);
            regenDelayTimer = regenDelay;

            if (currentValue <= 0f)
            {
                isExhausted = true;
            }
        }
        else if (regenDelayTimer > 0f)
        {
            regenDelayTimer -= Time.deltaTime;
        }
        else
        {
            Modify(regenPerSecond * Time.deltaTime);
        }

        if (isExhausted && currentValue >= maxValue * exhaustedRecoveryFraction)
        {
            isExhausted = false;
        }
    }
}

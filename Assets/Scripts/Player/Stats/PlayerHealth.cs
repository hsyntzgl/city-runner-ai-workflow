using UnityEngine;

public class PlayerHealth : StatSystem
{
    public bool IsDead => currentValue <= 0f;

    public void TakeDamage(float amount)
    {
        if (amount <= 0f || IsDead)
        {
            return;
        }

        Modify(-amount);
    }

    public void Heal(float amount)
    {
        if (amount <= 0f)
        {
            return;
        }

        Modify(amount);
    }
}

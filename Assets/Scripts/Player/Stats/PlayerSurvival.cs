using UnityEngine;

// Temporary stand-in for a real damage/healing system. Right now the only way
// health can drop is by neglecting hunger/sleep; once combat and medicine exist,
// they should call PlayerHealth.TakeDamage()/Heal() directly and this script can
// stay as-is (or be removed) without touching those systems.
[RequireComponent(typeof(PlayerHealth))]
public class PlayerSurvival : MonoBehaviour
{
    [SerializeField] private float starvationDamagePerSecond = 2f;
    [SerializeField] private float exhaustionDamagePerSecond = 1f;

    private PlayerHealth health;
    private PlayerHunger hunger;
    private PlayerSleep sleep;

    private void Awake()
    {
        health = GetComponent<PlayerHealth>();
        hunger = GetComponent<PlayerHunger>();
        sleep = GetComponent<PlayerSleep>();
    }

    private void Update()
    {
        if (health.IsDead)
        {
            return;
        }

        float damagePerSecond = 0f;

        if (hunger != null && hunger.IsEmpty)
        {
            damagePerSecond += starvationDamagePerSecond;
        }

        if (sleep != null && sleep.IsEmpty)
        {
            damagePerSecond += exhaustionDamagePerSecond;
        }

        if (damagePerSecond > 0f)
        {
            health.TakeDamage(damagePerSecond * Time.deltaTime);
        }
    }
}

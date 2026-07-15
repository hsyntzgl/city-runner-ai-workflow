using UnityEngine;

public class PlayerSleep : StatSystem
{
    [SerializeField] private float drainPerSecond = 0.1f;

    private void Update()
    {
        Modify(-drainPerSecond * Time.deltaTime);
    }

    public void Rest(float amount)
    {
        Modify(amount);
    }
}

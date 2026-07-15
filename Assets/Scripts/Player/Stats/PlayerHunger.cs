using UnityEngine;

public class PlayerHunger : StatSystem
{
    [SerializeField] private float drainPerSecond = 0.2f;

    private void Update()
    {
        Modify(-drainPerSecond * Time.deltaTime);
    }
}

using UnityEngine;

// Placeholder readout so the stats are visible without a real UI yet.
// Swap for a proper Canvas-based HUD later; consumers should read
// StatSystem.Normalized / CurrentValue or subscribe to OnValueChanged.
public class PlayerStatsDebugUI : MonoBehaviour
{
    [SerializeField] private PlayerHealth health;
    [SerializeField] private PlayerHunger hunger;
    [SerializeField] private PlayerSleep sleep;
    [SerializeField] private PlayerStamina stamina;

    private void Awake()
    {
        if (health == null) health = GetComponent<PlayerHealth>();
        if (hunger == null) hunger = GetComponent<PlayerHunger>();
        if (sleep == null) sleep = GetComponent<PlayerSleep>();
        if (stamina == null) stamina = GetComponent<PlayerStamina>();
    }

    private void OnGUI()
    {
        const int width = 220;
        const int barHeight = 18;
        const int spacing = 26;
        int y = 10;

        DrawBar(ref y, width, barHeight, spacing, "Health", health, new Color(0.8f, 0.15f, 0.15f));
        DrawBar(ref y, width, barHeight, spacing, "Hunger", hunger, new Color(0.85f, 0.6f, 0.1f));
        DrawBar(ref y, width, barHeight, spacing, "Sleep", sleep, new Color(0.2f, 0.4f, 0.85f));
        DrawBar(ref y, width, barHeight, spacing, "Stamina", stamina, new Color(0.2f, 0.75f, 0.3f));
    }

    private static void DrawBar(ref int y, int width, int height, int spacing, string label, StatSystem stat, Color color)
    {
        if (stat == null)
        {
            return;
        }

        GUI.Box(new Rect(10, y, width, height), string.Empty);

        Color previous = GUI.color;
        GUI.color = color;
        GUI.DrawTexture(new Rect(12, y + 2, (width - 4) * stat.Normalized, height - 4), Texture2D.whiteTexture);
        GUI.color = previous;

        GUI.Label(new Rect(16, y, width, height), string.Format("{0}: {1:0}/{2:0}", label, stat.CurrentValue, stat.MaxValue));

        y += spacing;
    }
}

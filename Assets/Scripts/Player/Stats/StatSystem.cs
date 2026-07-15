using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class StatValueChangedEvent : UnityEvent<float, float> { }

public abstract class StatSystem : MonoBehaviour
{
    [SerializeField] protected float maxValue = 100f;
    [SerializeField] protected float currentValue = 100f;

    public StatValueChangedEvent OnValueChanged = new StatValueChangedEvent();

    public float CurrentValue => currentValue;
    public float MaxValue => maxValue;
    public float Normalized => maxValue > 0f ? currentValue / maxValue : 0f;
    public bool IsEmpty => currentValue <= 0f;
    public bool IsFull => currentValue >= maxValue;

    protected virtual void Awake()
    {
        currentValue = Mathf.Clamp(currentValue, 0f, maxValue);
    }

    public void Modify(float amount)
    {
        SetValue(currentValue + amount);
    }

    public void SetValue(float value)
    {
        float clamped = Mathf.Clamp(value, 0f, maxValue);
        if (Mathf.Approximately(clamped, currentValue))
        {
            return;
        }

        currentValue = clamped;
        OnValueChanged.Invoke(currentValue, maxValue);
    }
}

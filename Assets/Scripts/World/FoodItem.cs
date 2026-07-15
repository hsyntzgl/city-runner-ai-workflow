using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FoodItem : MonoBehaviour, IInteractable
{
    [SerializeField] private float hungerRestoreAmount = 20f;
    [SerializeField] private bool consumeOnUse = true;

    public void Interact(GameObject interactor)
    {
        PlayerHunger hunger = interactor.GetComponent<PlayerHunger>();
        if (hunger == null)
        {
            return;
        }

        hunger.Modify(hungerRestoreAmount);

        if (consumeOnUse)
        {
            Destroy(gameObject);
        }
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private float interactRange = 2.5f;
    [SerializeField] private float interactRadius = 0.4f;

    private InputAction interactAction;

    private void Awake()
    {
        InputActionMap playerMap = inputActions.FindActionMap("Player");
        interactAction = playerMap.FindAction("Interact");
    }

    private void OnEnable()
    {
        interactAction.Enable();
        interactAction.performed += OnInteractPerformed;
    }

    private void OnDisable()
    {
        interactAction.performed -= OnInteractPerformed;
        interactAction.Disable();
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        Interact();
    }

    private void Interact()
    {
        Vector3 origin = transform.position + Vector3.up;
        if (Physics.SphereCast(origin, interactRadius, transform.forward, out RaycastHit hit, interactRange))
        {
            IInteractable interactable = hit.collider.GetComponentInParent<IInteractable>();
            interactable?.Interact(gameObject);
        }
    }
}

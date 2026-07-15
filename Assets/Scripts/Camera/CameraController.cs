using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private enum ViewMode
    {
        ThirdPerson,
        FirstPerson
    }

    [Header("Input")]
    [SerializeField] private InputActionAsset inputActions;

    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Look")]
    [SerializeField] private float mouseSensitivity = 0.15f;
    [SerializeField] private float minPitch = -40f;
    [SerializeField] private float maxPitch = 75f;

    [Header("Third Person")]
    [SerializeField] private float thirdPersonDistance = 4f;
    [SerializeField] private Vector3 thirdPersonPivotOffset = new Vector3(0f, 1.6f, 0f);

    [Header("First Person")]
    [SerializeField] private Vector3 firstPersonOffset = new Vector3(0f, 1.6f, 0f);

    private InputAction lookAction;
    private InputAction toggleViewAction;
    private ViewMode currentView = ViewMode.ThirdPerson;
    private float yaw;
    private float pitch;
    private Renderer[] targetRenderers;

    private void Awake()
    {
        InputActionMap playerMap = inputActions.FindActionMap("Player");
        lookAction = playerMap.FindAction("Look");
        toggleViewAction = playerMap.FindAction("ToggleView");

        if (target != null)
        {
            targetRenderers = target.GetComponentsInChildren<Renderer>();
            yaw = target.eulerAngles.y;
        }
    }

    private void OnEnable()
    {
        lookAction.Enable();
        toggleViewAction.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        ApplyViewMode();
    }

    private void OnDisable()
    {
        lookAction.Disable();
        toggleViewAction.Disable();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        if (toggleViewAction.WasPerformedThisFrame())
        {
            currentView = currentView == ViewMode.ThirdPerson ? ViewMode.FirstPerson : ViewMode.ThirdPerson;
            ApplyViewMode();
        }

        Vector2 lookDelta = lookAction.ReadValue<Vector2>();
        yaw += lookDelta.x * mouseSensitivity;
        pitch = Mathf.Clamp(pitch - lookDelta.y * mouseSensitivity, minPitch, maxPitch);
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        Quaternion lookRotation = Quaternion.Euler(pitch, yaw, 0f);

        if (currentView == ViewMode.FirstPerson)
        {
            transform.SetPositionAndRotation(target.position + firstPersonOffset, lookRotation);
            target.rotation = Quaternion.Euler(0f, yaw, 0f);
        }
        else
        {
            Vector3 pivot = target.position + thirdPersonPivotOffset;
            Vector3 position = pivot - lookRotation * Vector3.forward * thirdPersonDistance;
            transform.SetPositionAndRotation(position, lookRotation);
        }
    }

    private void ApplyViewMode()
    {
        if (targetRenderers == null)
        {
            return;
        }

        bool showBody = currentView == ViewMode.ThirdPerson;
        foreach (Renderer r in targetRenderers)
        {
            r.enabled = showBody;
        }
    }
}

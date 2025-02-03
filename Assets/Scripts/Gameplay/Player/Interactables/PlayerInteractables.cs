using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Gameplay.Managers;

public class PlayerInteractables : MonoBehaviour
{
    private const int FramesNeededToUpdate = 10;

    [SerializeField] private float maxDistance = 1f;
    [SerializeField] private LayerMask layers;

    private bool isTextWindowVisible = false;
    private Interactable currentInteractable;
    private int frameCounter;
    private InputActions inputActions;
    private Transform cameraTransform;

    private void Awake()
    {
        cameraTransform = Camera.main?.transform;
    }

    private void OnEnable()
    {
        inputActions = new InputActions();
        inputActions.Player.Enable();
        inputActions.Player.Use.performed += OnInteract;
    }

    private void OnDisable()
    {
        inputActions.Player.Use.performed -= OnInteract;
        inputActions.Player.Disable();
    }

    private void Update()
    {
        if (frameCounter++ < FramesNeededToUpdate) return;
        frameCounter = 0;

        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hitInfo, maxDistance, layers))
        {
            if (hitInfo.collider.TryGetComponent(out Interactable interactable) && interactable.active)
            {
                EventsManager.Instance.SetInterctableViewText.Invoke(interactable?.GetHoverText());
                EventsManager.Instance.ToggleInteractableViewVisibity.Invoke(isTextWindowVisible);
                isTextWindowVisible = true;
                currentInteractable = interactable;
            }
        }
        else
        {
            if (!isTextWindowVisible) { return; }
            isTextWindowVisible = false;
            currentInteractable = null;
            EventsManager.Instance.ToggleInteractableViewVisibity.Invoke(isTextWindowVisible);
        }
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        currentInteractable?.Interact();
    }
}
using System;
using Laywelin;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Laywelin {
  public class PlayerInteractionController : MonoBehaviour {
    [SerializeField] private float maxInteractionDistance = 1.5f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private InputHandler inputHandler;

    public Action<IInteractableElement> OnInteractionTargetChanged;
    private GameObject currentInteractableObject = null;

    private Interactable currentInteractable;

    private void Start() {
      inputHandler.OnGameplayInteractPressed += DefaultInteractionHandler;
    }

    private void Update() {
      DetectInteractableElement();
    }

    private void DetectInteractableElement() {
      if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, maxInteractionDistance)
          && hit.collider.gameObject != currentInteractableObject
          && hit.collider.TryGetComponent(out IInteractableElement newTarget)
          && newTarget.CanInteractWith) {
        currentInteractableObject = hit.collider.gameObject;
        if (newTarget is Interactable interactable)
          currentInteractable = interactable;
        OnInteractionTargetChanged?.Invoke(newTarget);
        return;
      }

      if (currentInteractableObject != null) {
        currentInteractableObject = null;
        currentInteractable = null;
        OnInteractionTargetChanged?.Invoke(null);
      }
    }

    private void DefaultInteractionHandler() {
      if (currentInteractable == null)
        return;

      currentInteractable.Interact();
    }
  }
}
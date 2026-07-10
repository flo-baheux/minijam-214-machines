using System;
using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Laywelin {
  public class PlayerInteractionController : MonoBehaviour {
    [SerializeField] private float maxInteractionDistance = 1.5f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform world;
    
    public Action<IInteractableElement> OnInteractionTargetChanged;
    private GameObject currentInteractableObject = null;
    private Interactable currentInteractable;
    
    private InputHandler inputHandler;

    private void Start() {
      inputHandler = DependencyManager.Instance.InputHandler;
      inputHandler.OnGameplayInteractPressed += DefaultInteractionHandler;
      inputHandler.OnGameplayRotateWorldLeftPressed += RotateWorldLeftHandler;
      inputHandler.OnGameplayRotateWorldRightPressed += RotateWorldRightHandler;
    }

    private void Update() {
      DetectInteractableElement();
    }

    private void DetectInteractableElement() {
      Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

      if (Physics.Raycast(ray, out RaycastHit hit, maxInteractionDistance)) {
        if (hit.collider.TryGetComponent(out IInteractableElement newTarget) && newTarget.CanInteractWith) {
          if (hit.collider.gameObject == currentInteractableObject)
            return;

          currentInteractableObject = hit.collider.gameObject;
          if (newTarget is Interactable interactable)
            currentInteractable = interactable;

          OnInteractionTargetChanged?.Invoke(newTarget);
          return;
        }
      }
      
      // now if there was no hit, or not interactable. Did you had an object? If so, remove it.
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

    private void RotateWorldLeftHandler() {
      inputHandler.inputContext = InputContext.CUTSCENE;
      world.DOLocalRotate(Vector3.up * -90f, 1f, RotateMode.LocalAxisAdd).SetEase(Ease.OutQuad).OnComplete(() => { 
        inputHandler.inputContext = InputContext.GAMEPLAY;
      });
    }

    private void RotateWorldRightHandler() {
      inputHandler.inputContext = InputContext.CUTSCENE;
      world.DOLocalRotate(Vector3.up * 90f, 1f, RotateMode.LocalAxisAdd).SetEase(Ease.OutQuad).OnComplete(() => {
        inputHandler.inputContext = InputContext.GAMEPLAY;
      });
    }
  }
}
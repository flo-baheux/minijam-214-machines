using System;
using UnityEngine;

namespace Laywelin {
  public class ItemHoldController : MonoBehaviour {
    [SerializeField] private PlayerInteractionController interactionController;
    [SerializeField] private InputHandler inputHandler;
    
    [SerializeField] private Transform anchor;

    public Action<Holdable> OnHoldingItem, OnDroppingItem;
    
    private Holdable holdableTarget;
    private Holdable heldItem;
    
    private void Start() {
      inputHandler.OnGameplayInteractPressed += OnGameplayInteractPressedHandler;
      interactionController.OnInteractionTargetChanged += OnInteractionTargetChangedHandler;
    }

    private void OnGameplayInteractPressedHandler() {
      if (heldItem != null) {
        heldItem.Drop();
        OnDroppingItem?.Invoke(heldItem);
        heldItem = null;
        return;
      }

      if (holdableTarget != null && holdableTarget.CanInteractWith) {
        heldItem = holdableTarget;
        heldItem.Hold(anchor);
        OnHoldingItem?.Invoke(heldItem);
      }
    }

    private void OnInteractionTargetChangedHandler(IInteractableElement target) {
      if (target is Holdable holdable)
        holdableTarget = holdable;
      else
        holdableTarget = null;
    }
  }
}
using UnityEngine;

namespace Laywelin {
  public class Holdable : MonoBehaviour, IInteractableElement {
    public bool CanInteractWith { get; protected set; } = true;

    [SerializeField] private Rigidbody rb;

    private Transform anchor;
    private bool IsHeld;

    public virtual void Hold(Transform anchor) {
      this.anchor = anchor;
      rb.freezeRotation = true;
      rb.isKinematic = true;
      IsHeld = true;
    }

    public virtual void Drop(Transform targetAnchor = null) {
      rb.freezeRotation = false;
      rb.isKinematic = false;
      IsHeld = false;
      anchor = targetAnchor;
    }

    private void LateUpdate() {
      if (!IsHeld)
        return;
      rb.MovePosition(anchor.position);
    }
  }
}
using Laywelin;
using UnityEngine;
using UnityEngine.Events;

public class BarWineGlassInteractable : Interactable {
  [SerializeField] private Animator animator;
  public UnityEvent OnComplete;

  private void Awake() {
    animator.enabled = false;
  }

  public override void Interact() {
    base.Interact();
    TryOpen();
  }

  public void TryOpen() {
    animator.enabled = true;
    OnComplete?.Invoke();
  }
}

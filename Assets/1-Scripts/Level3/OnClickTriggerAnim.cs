using Laywelin;
using UnityEngine;
using UnityEngine.Events;

public class OnClickTriggerAnim : Interactable {
  [SerializeField] private Animator animator;
  public UnityEvent OnComplete;

  private void Awake() {
    animator.enabled = false;
  }

  public override void Interact() {
    base.Interact();
    animator.enabled = true;
  }
}

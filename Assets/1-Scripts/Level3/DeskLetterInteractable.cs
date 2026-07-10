using DG.Tweening;
using Laywelin;
using UnityEngine;
using UnityEngine.Events;

public class DeskLetterInteractable : Interactable {
  [SerializeField] private Animator animator;
  [SerializeField] private Transform letterTransform;
  [SerializeField] private ItemData requiredItem;
  public UnityEvent OnComplete;

  private void Awake() {
    animator.enabled = false;
  }

  public override void Interact() {
    base.Interact();
    TryOpen();
  }

  public void TryOpen() {

    if (!GlobalGameManager.Instance.PlayerInventoryController.TryRemoveItem(requiredItem, 1)) {
      letterTransform.DOShakeRotation(1f, new Vector3(0, 1, 0), 10, 12, false, ShakeRandomnessMode.Harmonic);
      return;
    }

    animator.enabled = true;
    OnComplete?.Invoke();
  }
}

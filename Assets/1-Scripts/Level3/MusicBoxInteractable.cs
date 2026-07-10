using DG.Tweening;
using Laywelin;
using UnityEngine;
using UnityEngine.Events;

public class MusicBoxInteractable : Interactable {
  [SerializeField] private Animator animator;
  [SerializeField] private Transform lockTransform, pivot;
  [SerializeField] private ItemData requiredItem;
  [SerializeField] private AudioClip failOpenAudio, successOpenAudio;
  [SerializeField] private AudioSource audioSource;
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
      pivot.DOShakeRotation(1f, new Vector3(0, 1, 0), 10, 12, false, ShakeRandomnessMode.Harmonic);
      lockTransform.DOShakePosition(1f, new Vector3(0.1f, 0.1f, 0.1f), 10, 12, false, false, ShakeRandomnessMode.Harmonic);
      return;
    }

    audioSource.PlayOneShot(successOpenAudio);
    animator.enabled = true;
    OnComplete?.Invoke();
  }
}

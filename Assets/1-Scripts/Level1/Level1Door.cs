using DG.Tweening;
using Laywelin;
using UnityEngine;
using UnityEngine.Events;

public class Level1Door : Interactable {
  [SerializeField] private Transform doorPivot;
  [SerializeField] private ItemData requiredItem;
  [SerializeField] private AudioClip lockedDoorAudio, openDoorAudio, unlockedDoorAudio;
  [SerializeField] private AudioSource audioSource;
  public bool isLockedByChain = true, isLockedByKey = true;

  public UnityEvent OnComplete;
  private void Awake() {
    doorPivot.localRotation = Quaternion.identity;
  }

  public void UnlockChain() {
    isLockedByChain = false;
  }

  public override void Interact() {
    base.Interact();
    TryOpen();
  }

  public void TryOpen() {

    if (isLockedByKey) {
      if (!GlobalGameManager.Instance.PlayerInventoryController.TryRemoveItem(requiredItem, 1)) { 
        doorPivot.DOShakeRotation(1f, new Vector3(0, 1, 0), 10, 12, false, ShakeRandomnessMode.Harmonic);
        audioSource.PlayOneShot(lockedDoorAudio);
        return;
      }
      isLockedByKey = false;
      audioSource.PlayOneShot(unlockedDoorAudio);
    }

    if (isLockedByChain) {
      doorPivot.DOShakeRotation(1f, new Vector3(0, 1, 0), 10, 12, false, ShakeRandomnessMode.Harmonic);
      audioSource.PlayOneShot(lockedDoorAudio);

      return;
    }

    OnComplete?.Invoke();
    DependencyManager.Instance.InputHandler.inputContext = InputContext.CUTSCENE;
    doorPivot.DOLocalRotate(new(0, 95f, 0), 2.5f).OnComplete(() => { 
      SceneTransitionManager.Instance.TransitionToScene("2-InYourHead");
    });
    audioSource.PlayOneShot(openDoorAudio);
  }
}

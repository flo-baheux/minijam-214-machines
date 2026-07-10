using DG.Tweening;
using Laywelin;
using UnityEngine;

public class Level2PipeRequireValve : Interactable {
  [SerializeField] private GameObject valve;
  [SerializeField] private ItemData requiredItem;
  // [SerializeField] private AudioClip lockedDoorAudio, openDoorAudio;
  
  private void Awake() {
    // doorPivot.localRotation = Quaternion.identity;
  }

  public override void Interact() {
    base.Interact();
    TryOpen();
  }

  public void TryOpen() {
    if (!GlobalGameManager.Instance.PlayerInventoryController.TryRemoveItem(requiredItem, 1)) {
      // audioSource.PlayOneShot(lockedDoorAudio);
      return;
    }

    valve.gameObject.SetActive(true);
    GetComponent<Collider>().enabled = false;
    GlobalGameManager.Instance.PlayerInventoryController.TryRemoveItem(requiredItem, 1);
    // audioSource.PlayOneShot(openDoorAudio);
  }
}

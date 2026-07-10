using DG.Tweening;
using Laywelin;
using UnityEngine;
using UnityEngine.Events;

public class Level1DoorChain : Interactable {
  [SerializeField] private GameObject nodeToDestroy;
  // [SerializeField] private AudioClip lockedDoorAudio, openDoorAudio;

  public UnityEvent OnComplete;
  public override void Interact() {
    base.Interact();
    Destroy(nodeToDestroy);
    OnComplete?.Invoke();
  }

}

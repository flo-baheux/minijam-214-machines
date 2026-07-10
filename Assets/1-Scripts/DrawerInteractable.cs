using DG.Tweening;
using Laywelin;
using UnityEngine;

public class DrawerInteractable : Interactable {
  [SerializeField] private Vector3 openPos, closePos;
  [SerializeField] private Collider onlyEnabledWhenOpen;
  [SerializeField] private AudioClip open, close;
  [SerializeField] private AudioSource audioSource;
  
  private bool isOpen;
  
  private void Awake() {
    transform.localPosition = closePos;
    isOpen = false;
    if (onlyEnabledWhenOpen != null)
      onlyEnabledWhenOpen.enabled = false;
  }

  public override void Interact() {
    base.Interact();
    Toggle();
  }

  public void Toggle() {
    audioSource.PlayOneShot(isOpen ? close : open);
    transform.DOKill(true);
    transform.DOLocalMove(isOpen ? closePos : openPos, 0.8f);
    isOpen = !isOpen;
    if (onlyEnabledWhenOpen != null)
      onlyEnabledWhenOpen.enabled = isOpen;
  }
}

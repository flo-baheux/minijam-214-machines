using System.Collections.Generic;
using DG.Tweening;
using Laywelin;
using UnityEngine;
using UnityEngine.Events;

public class Level2Valve : Interactable {
  [SerializeField] private Transform waterToScale, waterflowToScale, boxesToMove, valveToRotate;
  [SerializeField] private float waterTargetScaleY, boxesToMoveTargetPosY;
  [SerializeField] private Collider colliderToDisable;
  [SerializeField] private List<Collider> collidersToEnable;

  [SerializeField] private GameObject vfx1, vfx2;
  // [SerializeField] private AudioClip lockedDoorAudio, openDoorAudio;
  public UnityEvent OnComplete; 
  
  private void Awake() {
    foreach (var c in collidersToEnable)
      c.enabled = false;
  }

  public override void Interact() {
    base.Interact();
    colliderToDisable.enabled = false;
    DOTween.Sequence()
      .Append(valveToRotate.DOLocalRotate(new(-450, 0, 0), 1.5f).SetEase(Ease.Linear).SetRelative(true))
      .Insert(0.8f, waterflowToScale.DOScale(new Vector3(0f, waterflowToScale.transform.localScale.y, 0f), 1.5f).SetEase(Ease.Linear))
      .JoinCallback(() => {
        vfx1.SetActive(false);
        vfx2.SetActive(false);
      })
      .Insert(1.5f, waterToScale.DOScaleY(waterTargetScaleY, 2f))
      .Join(boxesToMove.DOMoveY(boxesToMoveTargetPosY, 2f))
      .OnComplete(() => { 
        collidersToEnable.ForEach(c => c.enabled = true);
        OnComplete?.Invoke();
      });
  }
}

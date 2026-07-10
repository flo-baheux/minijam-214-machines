using System.Collections.Generic;
using DG.Tweening;
using Laywelin;
using UnityEngine;
using UnityEngine.Events;

public class Level2ElectricityLever : Interactable {
  [SerializeField] private ItemData requiredItem;
  [SerializeField] private Level2CableMissing cableMissingPuzzle;
  [SerializeField] private Transform leverPivot;
  [SerializeField] private AudioSource audioSource;
  [SerializeField] private AudioClip clicLever, machineStart, machineRun, lightOn;
  
  public UnityEvent OnComplete; 
  
  public override void Interact() {
    base.Interact();
    if (!cableMissingPuzzle.isCompleted) {
      leverPivot.DOShakeRotation(1f, new Vector3(1, 0, 0), 10, 12, false, ShakeRandomnessMode.Harmonic);
      return;
    }

    leverPivot.DOLocalRotate(new(-100, 0, 0), 2f).SetRelative(true).SetEase(Ease.OutBack);
    OnComplete?.Invoke();
    GetComponent<Collider>().enabled = false;

    DOTween.Sequence()
      .InsertCallback(0, () => audioSource.PlayOneShot(clicLever))
      .InsertCallback(0, () => audioSource.PlayOneShot(lightOn))
      .InsertCallback(0f, () => audioSource.PlayOneShot(machineStart))
      .InsertCallback(0f + 8.722f, () => {
        audioSource.clip = machineRun;
        audioSource.Play();
      });
  }
}

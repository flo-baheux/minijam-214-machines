using System.Collections.Generic;
using DG.Tweening;
using Laywelin;
using UnityEngine;
using UnityEngine.Events;

public class Level2Cardboard : Interactable {
  [SerializeField] private AudioClip cardboardAudio;
  [SerializeField] private AudioSource audioSource;
  
  public override void Interact() {
    base.Interact();
    audioSource.PlayOneShot(cardboardAudio);
    transform.DOScale(0, 1f).OnComplete(() => Destroy(gameObject, 2));
  }
}

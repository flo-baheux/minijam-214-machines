using System;
using DG.Tweening;
using Laywelin;
using UnityEngine;

public class DoubleSlidingDoor : MonoBehaviour {
  [SerializeField] private Transform doorL, doorR;

  [SerializeField]
  private AudioSource audioSource;

  [SerializeField] private AudioClip openDoorClip;
  public void Open() {
    doorL.DOKill();
    doorR.DOKill();
    audioSource.PlayOneShot(openDoorClip);
    DOTween.Sequence()
      .Append(doorL.DOLocalMove(new(0, 0, -0.2f), 2f).SetRelative(true))
      .Join(doorR.DOLocalMove(new(0, 0, 0.2f), 2f).SetRelative(true))
      .OnComplete(() => {
        DependencyManager.Instance.InputHandler.inputContext = InputContext.CUTSCENE;
        SceneTransitionManager.Instance.TransitionToScene("3-InYourHeart");
      });
  }

  public void FailOpen() {
    doorL.DOKill();
    doorR.DOKill();
    DOTween.Sequence()
      .Append(doorL.DOShakeRotation(1f, new Vector3(0, 0, -0.05f), 10, 12, false, ShakeRandomnessMode.Harmonic))
      .Join(doorR.DOShakeRotation(1f, new Vector3(0, 0, 0.05f), 10, 12, false, ShakeRandomnessMode.Harmonic));
  }
}

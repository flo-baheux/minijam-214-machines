using System;
using DG.Tweening;
using Laywelin;
using UnityEngine;

public class HeartPieceProceduralAnimation : MonoBehaviour {
  [SerializeField] private Camera cam;
  [SerializeField] private Transform targetOnTrigger;
  [SerializeField] private GameObject vfx;

  private void Awake() {
    vfx.SetActive(false);
  }

  public void Spawn() {
    DOTween.Sequence()
      .Append(transform.DOScale(1, 1.5f).From(0).SetEase(Ease.OutBack))
      .Join(transform.DOLocalMoveY(1f, 1.5f).SetRelative(true).SetEase(Ease.OutBack))
      .Append(transform.DOMove(targetOnTrigger.position, 1f))
      .OnComplete(() => {
        transform.SetParent(targetOnTrigger);
        vfx.SetActive(true);
        GlobalGameManager.Instance.FoundHeartPiece();
        transform.localPosition = Vector3.zero;
      });

    Color.RGBToHSV(cam.backgroundColor, out float h, out float s, out float v);
    
    Color targetColor = Color.HSVToRGB(h, s, Mathf.Clamp01(v + 0.12f));
    DOTween.To(
      () => cam.backgroundColor,
      x => cam.backgroundColor = x,
      targetColor,
      1.5f
    );
  }
}

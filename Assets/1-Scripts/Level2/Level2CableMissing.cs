using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Laywelin;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Splines;

public class Level2CableMissing : Interactable {
  [SerializeField] private ItemData requiredItem;
  [SerializeField] private SplineContainer splineContainer;
  
  public UnityEvent OnComplete;

  public bool isCompleted = false;
  
  public override void Interact() {
    base.Interact();
    if (!GlobalGameManager.Instance.PlayerInventoryController.TryRemoveItem(requiredItem, 1))
      return;
    
    OnComplete?.Invoke();
  
    GetComponent<Collider>().enabled = false;
    isCompleted = true;

    StartCoroutine(MoveKnotRoutine());
  }

  private IEnumerator MoveKnotRoutine() {
    var spline = splineContainer.Spline;

    BezierKnot knot = spline[4];
    Vector3 startPos = knot.Position;
    Vector3 targetPos = new Vector3(-1.879f, 2.175f, 8.508f);
    float t = 0f;

    while (t < 1f) {
      t += Time.deltaTime / 2f;

      knot = spline[4];
      knot.Position = Vector3.Lerp(startPos, targetPos, t);
      spline[4] = knot;

      yield return null;
    }

    knot = spline[4];
    knot.Position = targetPos;
    spline[4] = knot;
  }
}

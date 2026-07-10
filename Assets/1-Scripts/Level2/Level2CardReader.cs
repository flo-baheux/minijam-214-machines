using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Laywelin;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Splines;

public class Level2CardReader : Interactable {
  [SerializeField] private ItemData requiredItem;
  public UnityEvent failToComply, OnComplete;

  public override void Interact() {
    base.Interact();
    if (!GlobalGameManager.Instance.PlayerInventoryController.TryRemoveItem(requiredItem, 1)) {
      failToComply?.Invoke();
      return;
    }

    OnComplete?.Invoke();
    GetComponent<Collider>().enabled = false;
  }
}

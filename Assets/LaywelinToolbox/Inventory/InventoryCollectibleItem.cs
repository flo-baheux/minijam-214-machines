using UnityEngine;

namespace Laywelin {
  public class InventoryCollectibleItem: Interactable {
    [SerializeField] private ItemData item;

    public override void Interact() {
      if (!GlobalGameManager.Instance.PlayerInventoryController.TryAddItem(item, 1))
        return;
      CanInteractWith = false;
      Destroy(gameObject);
    }
  }
}
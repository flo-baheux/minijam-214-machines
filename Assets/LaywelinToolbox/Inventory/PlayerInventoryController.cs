namespace Laywelin {
  public class PlayerInventoryController {
    private readonly Inventory inventory = new();

    public bool TryAddItem(ItemData item, int quantity) {
      int countBefore = inventory.Count(item);

      if (!inventory.TryAddItem(item, quantity))
        return false;

      EventBusManager.Emit(new PlayerInventoryItemAddedEvent() {
        item = item,
        quantityBefore = countBefore,
        quantityAfter = inventory.Count(item)
      });
      
      return true;
    }

    public bool TryRemoveItem(ItemData item, int quantity) {
      int countBefore = inventory.Count(item);

      if (!inventory.TryRemoveItem(item, quantity))
        return false;

      EventBusManager.Emit(new PlayerInventoryItemRemovedEvent() {
        item = item,
        quantityBefore = countBefore,
        quantityAfter = inventory.Count(item)
      });

      return true;
    }
  }
}
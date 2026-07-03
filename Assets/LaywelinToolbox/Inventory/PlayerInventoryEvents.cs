using UnityEngine;

namespace Laywelin {
  public class PlayerInventoryItemAddedEvent : GameEvent {
    public ItemData item;
    public int quantityBefore, quantityAfter;
  }

  public class PlayerInventoryItemRemovedEvent : GameEvent {
    public ItemData item;
    public int quantityBefore, quantityAfter;
  }
}
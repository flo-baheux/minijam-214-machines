using System;
using System.Collections.Generic;

namespace Laywelin {
  public class Inventory {
    private readonly Dictionary<ItemData, int> storage = new();
    
    public bool TryAddItem(ItemData item, int quantity) {
      if (item == null || quantity <= 0)
        return false;

      if (!storage.TryAdd(item, quantity))
        storage[item] += quantity;

      return true;
    }

    public bool TryRemoveItem(ItemData item, int quantity) {
      if (!storage.TryGetValue(item, out int count))
        return false;
      
      if (quantity <= 0 || count < quantity)
        return false;

      if (count - quantity <= 0)
        storage.Remove(item);
      else
        storage[item] -= quantity;
      
      return true;
    }

    public int Count(ItemData item)
      => storage.GetValueOrDefault(item, 0);

    public bool ContainsItem(ItemData item)
      => Count(item) > 0;
  }
}
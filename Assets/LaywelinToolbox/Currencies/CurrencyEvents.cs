using System;
using UnityEngine;

namespace Laywelin.Currencies {
  public class CurrencyQuantityChangedEvent : GameEvent {
    public Currency currency;
    public int before, after;
  }

  public class CurrencyUnlockedEvent : GameEvent {
    public Currency currency;
  }
}
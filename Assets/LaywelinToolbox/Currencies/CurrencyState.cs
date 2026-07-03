using System;

namespace Laywelin.Currencies {
  [Serializable]
  public class CurrencyState {
    private Currency currency;

    public event Action<CurrencyQuantityChangedEvent> OnQuantityChanged;
    public event Action<CurrencyUnlockedEvent> OnUnlocked;

    public bool IsUnlocked { get; private set; }

    private int _quantity;

    public int Quantity {
      get => _quantity;
      private set => _quantity = Math.Max(value, 0);
    }

    public CurrencyState(Currency currency) {
      this.currency = currency;

      IsUnlocked = currency.UnlockedByDefault;
      Quantity = 0;
    }

    public bool TryAdjustQuantity(int adjustBy) {
      if (!IsUnlocked)
        return false;

      int oldValue = Quantity;
      Quantity += adjustBy;

      if (Quantity == oldValue)
        return false;

      OnQuantityChanged?.Invoke(new() {
        currency = currency,
        before = oldValue,
        after = Quantity
      });
      return true;
    }

    public void Unlock() {
      if (IsUnlocked)
        return;

      IsUnlocked = true;
      OnUnlocked?.Invoke(new() { currency = currency });
    }

    public void HydrateSaveData(CurrencySaveData saveData) {
      IsUnlocked = saveData.isUnlocked;
      Quantity = saveData.quantity;
    }

    public CurrencySaveData GetSaveData() => new() {
      currencyId = currency.CurrencyId,
      isUnlocked = IsUnlocked,
      quantity = Quantity,
    };

    public void ResetSaveData() {
      IsUnlocked = currency.UnlockedByDefault;
      Quantity = 0;
    }
  }
}
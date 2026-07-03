using System;
using System.Collections.Generic;

namespace Laywelin.Currencies {
  [Serializable]
  public class CurrenciesSaveData {
    public List<CurrencySaveData> currenciesSaveData;
  }

  [Serializable]
  public class CurrencySaveData {
    public string currencyId;
    public bool isUnlocked;
    public int quantity;
  }
}
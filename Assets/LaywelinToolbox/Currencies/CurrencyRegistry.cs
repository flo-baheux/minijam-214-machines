using System;
using System.Collections.Generic;
using UnityEngine;

namespace Laywelin.Currencies { 
  [DefaultExecutionOrder(-10)]
  public class CurrencyRegistry: MonoBehaviour {
      [SerializeField] private Currency[] currencies;
      
      private void Awake() {
        foreach (var currency in currencies)
          currency.InitState();
      }

      public void Init(List<CurrencySaveData> currenciesSaveData = null) {
        Dictionary<string, CurrencySaveData> saveDataById = new();

        if (currenciesSaveData != null) {
          foreach (var data in currenciesSaveData)
            saveDataById.Add(data.currencyId, data);
        }

        foreach (var currency in currencies)
          currency.InitState(saveDataById.GetValueOrDefault(currency.CurrencyId, null));
      }
  }
}
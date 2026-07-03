using System;
using UnityEngine;

namespace Laywelin.Currencies {
  [CreateAssetMenu(fileName = "Currency", menuName = "Laywelin/Currency")]
  public class Currency : ScriptableObject {
    [SerializeField] private string currencyId;
    public string CurrencyId => currencyId;

    [SerializeField] private string displayName;
    public string DisplayName => displayName;

    [SerializeField] private Sprite displayIcon;
    public Sprite DisplayIcon => displayIcon;

    [SerializeField] private bool unlockedByDefault = true;
    public bool UnlockedByDefault => unlockedByDefault;

    [NonSerialized] private CurrencyState state;

    public CurrencyState State {
      get {
        if (state == null)
          throw new InvalidOperationException($"Accessing uninitialized currency state for {displayName}.");
        return state;
      }
    }

    public void InitState(CurrencySaveData saveData = null) {
      state = new(this);
      if (saveData != null)
        state.HydrateSaveData(saveData);
    }
  }
}
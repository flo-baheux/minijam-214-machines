using DG.Tweening;
using Laywelin;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelectionChangedGameEvent : GameEvent {
  public ItemData previousItem;
  public ItemData currentItem;
}

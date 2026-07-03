using System.Collections.Generic;
using Laywelin;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI: MonoBehaviour {
    [SerializeField] private InventoryItemUI itemUIPrefab;
    [SerializeField] private Transform itemContainer;
    private Dictionary<ItemData, InventoryItemUI> itemUIBySO = new();
    
    private void Start() {
      EventBusManager.AddListener<PlayerInventoryItemAddedEvent>(OnItemAddedHandler);
      EventBusManager.AddListener<PlayerInventoryItemRemovedEvent>(OnItemRemovedHandler);
    }

    private void OnItemAddedHandler(PlayerInventoryItemAddedEvent evt) {
      var itemUI = Instantiate(itemUIPrefab, itemContainer);
      itemUI.Init(evt.item);
      itemUIBySO.Add(evt.item, itemUI);
    }

    private void OnItemRemovedHandler(PlayerInventoryItemRemovedEvent evt) { 
      if (!itemUIBySO.Remove(evt.item, out var itemUI))
        return;

      Destroy(itemUI.gameObject);
    }
}
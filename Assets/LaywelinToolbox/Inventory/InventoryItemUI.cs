using Laywelin;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemUI: MonoBehaviour {
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private Image icon;

    public void Init(ItemData item) {
      name.text = item.displayName;
      icon.sprite = item.displayIcon;
    }
  }
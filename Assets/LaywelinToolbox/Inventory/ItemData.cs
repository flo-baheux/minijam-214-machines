using UnityEngine;

namespace Laywelin {
  [CreateAssetMenu(fileName = "ItemData", menuName = "Laywelin/Data/Item")]
  public class ItemData: ScriptableObject {
    public string id;
    public string displayName;
    public Sprite displayIcon;
  }
}
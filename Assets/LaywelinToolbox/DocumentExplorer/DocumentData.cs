using System.Collections.Generic;
using UnityEngine;

namespace Laywelin {
  [CreateAssetMenu(fileName = "DocumentData", menuName = "Laywelin/SO/Document")]
  public class DocumentData: ScriptableObject {
      public bool isDoublePageDocument;
      public List<Sprite> documentImages = new();
  }
}
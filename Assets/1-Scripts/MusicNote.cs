using DG.Tweening;
using UnityEngine;


public enum MusicNoteEnum { 
  A,
  B,
  C,
  D,
  E,
  F,
  G
}

public class MusicNote : MonoBehaviour {
  [SerializeField] private Transform[] slots;
  [SerializeField] private Transform note;
  
  public void Set(MusicNoteEnum n) {
    switch (n) {
      case MusicNoteEnum.A:
        note.SetParent(slots[0]);
        break;
      case MusicNoteEnum.B:
        note.SetParent(slots[1]);
        break;
      case MusicNoteEnum.C:
        note.SetParent(slots[2]);
        break;
      case MusicNoteEnum.D:
        note.SetParent(slots[3]);
        break;
      case MusicNoteEnum.E:
        note.SetParent(slots[4]);
        break;
      case MusicNoteEnum.F:
        note.SetParent(slots[5]);
        break;
      case MusicNoteEnum.G:
        note.SetParent(slots[6]);
        break;
      default:
        break;
    }

    note.transform.localPosition = Vector3.zero;
    note.gameObject.SetActive(true);
    note.DOScale(1, 1f).From(2);
  }


  public void Reset() { 
    note.gameObject.SetActive(false);
  }
}

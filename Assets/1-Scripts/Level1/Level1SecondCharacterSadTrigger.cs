using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Level1SecondCharacterSadTrigger : MonoBehaviour {

  public void Trigger() {
    transform.DOLocalRotate(new(35, 0, 0), 2).SetRelative(true);
  }

}

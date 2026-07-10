using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class MainCharacterNameChangeAnim : MonoBehaviour {
  [SerializeField] private TextMeshPro text;
  [SerializeField] private Color defaultColor, alertColor;

  public void Trigger() {
    text.DOText("Sarah M.", 1.5f, scrambleMode: ScrambleMode.All);
    text.text = "Sarah M.";
    StartCoroutine(BlinkRoutine());
  }

  private IEnumerator BlinkRoutine() {
    for (int i = 0; i < 3; i++) {
      text.color = defaultColor;
      yield return new WaitForSeconds(1f);
      text.color = alertColor;
      yield return new WaitForSeconds(0.5f);
    }

    text.color = alertColor;
  }
}

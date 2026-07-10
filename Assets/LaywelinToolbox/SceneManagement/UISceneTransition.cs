using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Laywelin {
  public class UISceneTransition : MonoBehaviour {
    [SerializeField] private RectTransform sceneTransitionContainer;
    [SerializeField] private CanvasGroup canvas, delayedCanvasGroup;
    [SerializeField] private Image progressBarFill;
    [SerializeField] private TextMeshProUGUI progressPercentageText;

    private void Awake() {
      progressBarFill.fillAmount = 0;
      progressPercentageText.text = "";
      canvas.Toggle(false);
      delayedCanvasGroup.Toggle(false);
    }

    public void ShowTransition(Action callback = null) {
      delayedCanvasGroup.DOKill();
      delayedCanvasGroup.Toggle(false);
      canvas.DOFade(1, 2f).SetEase(Ease.Linear).OnComplete(() => {
        callback?.Invoke();
        delayedCanvasGroup.DOFade(1, 0.4f).From(0);
      });
    }

    public void HideTransition(Action callback = null) {
      delayedCanvasGroup.DOKill();
      delayedCanvasGroup.DOFade(0, 0.4f);
      canvas.DOFade(0, 2f).SetEase(Ease.Linear).OnComplete(() => callback?.Invoke());
    }

    private void Update() {
      if (SceneTransitionManager.Instance == null)
        return;

      if (!SceneTransitionManager.Instance.IsTransitioning)
        return;

      progressBarFill.fillAmount = SceneTransitionManager.Instance.sceneTransitionProgress;
      progressPercentageText.text = $"{SceneTransitionManager.Instance.sceneTransitionProgress * 100}%";

    }
  }
}
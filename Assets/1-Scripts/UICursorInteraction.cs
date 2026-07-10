using DG.Tweening;
using Laywelin;
using UnityEngine;
using UnityEngine.UI;

public class UICursorInteraction : MonoBehaviour {
  [SerializeField] private PlayerInteractionController playerInteraction;
  [SerializeField] private Image cursorIcon;
  [SerializeField] private Color canInteractColor, cannotInteractColor;
  [SerializeField] private float canInteractScale, cannotInteractScale;
  [SerializeField] private CanvasGroup canvasGroup;

  private Tween tween;

  private void Awake() {
    playerInteraction.OnInteractionTargetChanged += OnInteractionTargetChangedHandler;

    cursorIcon.color = cannotInteractColor;
    cursorIcon.transform.localScale = Vector3.one * cannotInteractScale;
  }
  
  private void OnInteractionTargetChangedHandler(IInteractableElement interactTarget) {
    tween?.Kill();

    bool canInteract = interactTarget != null;
    Color targetColor = canInteract ? canInteractColor : cannotInteractColor;
    float targetScale = canInteract ? canInteractScale : cannotInteractScale;

    tween = DOTween.Sequence()
      .Join(cursorIcon.DOColor(targetColor, 0.5f))
      .Join(cursorIcon.rectTransform.DOScale(targetScale, 0.5f));
  }
}

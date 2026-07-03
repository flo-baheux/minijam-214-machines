using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Laywelin {
  public class UIDocumentExplorer : MonoBehaviour {
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image documentImage;
    [SerializeField] private AudioClip pageChangeSound;
    [SerializeField] private GameObject nbPagesObject;
    [SerializeField] private TextMeshProUGUI nbPageIndicatorText;

    private void Awake() {
      canvasGroup.Toggle(false);
      nbPagesObject.SetActive(false);

      EventBusManager.AddListener<DocumentOpenedEvent>(DocumentOpenedEventHandler);
      EventBusManager.AddListener<DocumentClosedEvent>(DocumentClosedEventHandler);
      EventBusManager.AddListener<DocumentPageChangedEvent>(DocumentPageChangedEventHandler);
    }

    private void DocumentOpenedEventHandler(DocumentOpenedEvent evt) {
      ShowDocument(evt.document, evt.openingIndex);
    }

    private void DocumentClosedEventHandler(DocumentClosedEvent evt) {
      HideDocument();
    }

    private void DocumentPageChangedEventHandler(DocumentPageChangedEvent evt) {
      PageChange(evt.document, evt.previousIndex, evt.currentIndex);
    }

    private void ShowDocument(InteractableDocument document, int index) {
      documentImage.sprite = document.DocumentData.documentImages[index];
      canvasGroup.Toggle(true);
    }
    
    private void HideDocument() {
      canvasGroup.Toggle(false);
      documentImage.sprite = null;
    }

    public void PageChange(InteractableDocument document, int previousIndex, int currentIndex) {
      if (previousIndex == currentIndex)
        return;

      documentImage.sprite = document.DocumentData.documentImages[currentIndex];
    }
  }
}

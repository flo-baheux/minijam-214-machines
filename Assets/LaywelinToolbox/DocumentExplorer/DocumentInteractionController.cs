using System;
using UnityEngine;

namespace Laywelin {
  public class DocumentInteractionController : MonoBehaviour {
    private InteractableDocument document;
    private int currentIndex;

    private void Start() {
      EventBusManager.AddListener<DocumentOpenedEvent>(OpenedDocumentEventHandler);
      EventBusManager.AddListener<DocumentClosedEvent>(ClosedDocumentEventHandler);

      DependencyManager.Instance.InputHandler.OnDocumentNextPressed += NextPage;
      DependencyManager.Instance.InputHandler.OnDocumentPreviousPressed += PreviousPage;
    }

    private void OpenedDocumentEventHandler(DocumentOpenedEvent evt) {
      if (evt.document == null) {
        Debug.LogError("Cannot show document - document is null");
        return;
      }

      if (evt.document.DocumentData is null || evt.document.DocumentData.documentImages.Count == 0) {
        Debug.LogError("Cannot show document - No or empty document data");
        return;
      }
      document = evt.document;
      currentIndex = 0;
    }

    private void ClosedDocumentEventHandler(DocumentClosedEvent evt) {
      document = null;
      currentIndex = 0;
    }

    public void NextPage() {
      if (document == null)
        return;

      int previousIndex = currentIndex;
      currentIndex = Math.Clamp(currentIndex + 1, 0, document.DocumentData.documentImages.Count - 1);
      if (previousIndex == currentIndex)
        return;

      EventBusManager.Emit(new DocumentPageChangedEvent() { 
        document = document,
        previousIndex = previousIndex,
        currentIndex = currentIndex,
      });
    }

    public void PreviousPage() {
      if (document == null)
        return;

      int previousIndex = currentIndex;
      currentIndex = Math.Clamp(currentIndex - 1, 0, document.DocumentData.documentImages.Count - 1);
      if (previousIndex == currentIndex)
        return;

      EventBusManager.Emit(new DocumentPageChangedEvent() {
        document = document,
        previousIndex = previousIndex,
        currentIndex = currentIndex,
      });
    }

    private void OnDestroy() {
      EventBusManager.RemoveListener<DocumentOpenedEvent>(OpenedDocumentEventHandler);
      EventBusManager.RemoveListener<DocumentClosedEvent>(ClosedDocumentEventHandler);

      if (GlobalGameManager.Instance != null) {
        DependencyManager.Instance.InputHandler.OnDocumentNextPressed -= NextPage;
        DependencyManager.Instance.InputHandler.OnDocumentPreviousPressed -= PreviousPage;
      }
    }
  }
}

using System;
using UnityEngine;

namespace Laywelin {

  public class InteractableDocument : Interactable {
    [SerializeField] private DocumentData documentSO;
    public DocumentData DocumentData => documentSO;

    public bool IsInteractingWith { get; protected set; }

    public override void Interact() {
      IsInteractingWith = true;
      EventBusManager.Emit(new DocumentOpenedEvent() { document = this });
    }

    public void CloseDocument() {
      IsInteractingWith = false;
      EventBusManager.Emit(new DocumentClosedEvent() { document = this });
    }
  }
}
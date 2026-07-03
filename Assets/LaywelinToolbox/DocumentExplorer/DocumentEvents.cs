using System;
using UnityEngine;
using UnityEngine.UI;

namespace Laywelin {
  public enum DocumentFailureReason {
    ITEM_REQUIRED,
    //...
  }
  
  // Document can be a simple letter or a locked journal, so it could be locked.
  public class FailedToReadDocumentItemRequiredEvent : GameEvent { 
    public InteractableDocument document;
    // public requiredItem;
  }

  public class DocumentOpenedEvent: GameEvent {
    public InteractableDocument document;
    public int openingIndex = 0;
  }

  public class DocumentClosedEvent : GameEvent {
    public InteractableDocument document;
  }

  public class DocumentPageChangedEvent : GameEvent { 
    public InteractableDocument document;
    public int previousIndex, currentIndex;
  }
}

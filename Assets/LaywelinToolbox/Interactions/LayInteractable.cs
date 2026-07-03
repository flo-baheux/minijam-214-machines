using System;
using UnityEngine;

namespace Laywelin {
  public class Interactable: MonoBehaviour, IInteractableElement {
    public bool CanInteractWith { get; protected set; } = true;
    public Action<Interactable> OnInteractedWith;
    
    // public void Interact(InteractionContext interactionContext);
    public virtual void Interact() { 
      OnInteractedWith?.Invoke(this);
    }
  }
}


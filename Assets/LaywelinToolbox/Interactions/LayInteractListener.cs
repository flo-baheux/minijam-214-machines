using System;
using UnityEngine;
using UnityEngine.Events;

namespace Laywelin {
  public class InteractListener : MonoBehaviour {
    [SerializeField] private Interactable interactable;
    
    [SerializeField] private UnityEvent<Interactable> editorOnInteractionReceived;
    public event Action<Interactable> OnInteractionReceived = null!;

    private void Awake() {
      interactable.OnInteractedWith += OnInteractedWithHandler;
    }

    protected virtual void OnInteractedWithHandler(Interactable with) {
      OnInteractionReceived?.Invoke(with);
      editorOnInteractionReceived?.Invoke(with);
    }

    private void OnDestroy() {
      interactable.OnInteractedWith -= OnInteractedWithHandler;
    }
  }
}
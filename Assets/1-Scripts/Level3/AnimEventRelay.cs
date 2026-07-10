using UnityEngine;
using UnityEngine.Events;

public class AnimEventRelay : MonoBehaviour {
  public UnityEvent evt;

  public void Trigger() { 
    evt?.Invoke();
  }
}

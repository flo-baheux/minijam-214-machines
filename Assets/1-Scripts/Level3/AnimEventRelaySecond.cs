using UnityEngine;
using UnityEngine.Events;

public class AnimEventRelaySecond : MonoBehaviour {
  public UnityEvent evt;

  public void TriggerSecond() { 
    evt?.Invoke();
  }
}

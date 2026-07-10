using Laywelin;
using UnityEngine;

public class EndingScript : MonoBehaviour {
  [SerializeField] private Animator anim;

  private void Start() { 
    EventBusManager.AddListener<TriggerEndingGameEvent>(TriggerEndingHandler);
  }

  private void TriggerEndingHandler(TriggerEndingGameEvent _) {
    DependencyManager.Instance.InputHandler.inputContext = InputContext.CUTSCENE;
    anim.enabled = true;
  }
}

using UnityEngine;

namespace Laywelin {

  public class DependencyManager : MonoBehaviour {
    public static DependencyManager Instance { get; private set; }

    [SerializeField] private InputHandler _inputHandler;
    public InputHandler InputHandler => _inputHandler;

    private void Awake() {
      if (Instance != null && Instance != this) {
        Destroy(gameObject);
        return;
      }

      Instance = this;
      DontDestroyOnLoad(gameObject);
    }
  }
}
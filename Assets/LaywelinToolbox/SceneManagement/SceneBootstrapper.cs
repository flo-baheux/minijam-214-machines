using System.Collections;
using UnityEngine;

namespace Laywelin {
  public abstract class SceneBootstrapper : MonoBehaviour {
    private void Awake() {
      SceneTransitionManager.Instance.RegisterSceneBootstrapper(this);
    }

    public abstract IEnumerator Initialize();
  }
}
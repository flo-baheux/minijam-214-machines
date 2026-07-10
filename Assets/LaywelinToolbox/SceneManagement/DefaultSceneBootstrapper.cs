using System;
using System.Collections;
using UnityEngine;

namespace Laywelin {
  public class DefaultSceneBootstrapper : SceneBootstrapper {
    [SerializeField] private AudioClip bgm, ambiance;
    private void Awake() {
      SceneTransitionManager.Instance.RegisterSceneBootstrapper(this);
    }

    public override IEnumerator Initialize() {
      
      DependencyManager.Instance.InputHandler.inputContext = InputContext.GAMEPLAY;

      try {
        if (bgm != null)
          AudioManager.Instance.PlayBGM(bgm);
      
        if (ambiance != null)
          AudioManager.Instance.PlayOnceSFX(ambiance);
      } catch (Exception e) { }

      return null;
    }
  }
}
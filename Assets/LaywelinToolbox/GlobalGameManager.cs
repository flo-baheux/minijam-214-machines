using System;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

namespace Laywelin {

  public class TriggerEndingGameEvent : GameEvent { }

  public class GlobalGameManager : MonoBehaviour {
    public static GlobalGameManager Instance { get; private set; }

    public event Action OnGamePaused, OnGameResumed;

    private int nbHeartPieceCollected = 0;
    
    public PlayerInventoryController PlayerInventoryController { get; } = new();
    
    private bool isGamePaused;
    
    private void Awake() {
      if (Instance != null && Instance != this) {
        Destroy(gameObject);
        return;
      }

      Instance = this;
      DontDestroyOnLoad(gameObject);

      FramerateVSyncSetup();
      DOTweenSetup();
    }

    private void DOTweenSetup() {
      DOTween.SetTweensCapacity(1000, 500);
      DOTween.defaultAutoKill = true;
      DOTween.defaultTimeScaleIndependent = true;
    }

    private void FramerateVSyncSetup() {
      Application.targetFrameRate = 60;
      QualitySettings.vSyncCount = 1;
    }
    
    public void FoundHeartPiece() {
      nbHeartPieceCollected++;
      if (nbHeartPieceCollected == 4)
        EventBusManager.Emit(new TriggerEndingGameEvent());
    }

    public void QuitGame() {
#if UNITY_EDITOR
      EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
  }
  
}
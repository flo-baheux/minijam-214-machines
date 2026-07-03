using System;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

namespace Laywelin {

  public enum GameplayMode { 
    LOOK_AROUND,
    DOCUMENT,
    INTERACT_UI,
  }

  public class GlobalGameManager : MonoBehaviour {
    public static GlobalGameManager Instance { get; private set; }

    public event Action<GameplayMode, GameplayMode> OnGameplayModeChanged;
    public event Action OnGamePaused, OnGameResumed;

    public PlayerInventoryController PlayerInventoryController { get; } = new();

    private bool isGamePaused;
    private GameplayMode previousGameplayMode = GameplayMode.LOOK_AROUND;
    private GameplayMode _currentGameplayMode = GameplayMode.LOOK_AROUND;

    public GameplayMode CurrentGameplayMode {
      get => _currentGameplayMode;
      private set {
        if (_currentGameplayMode == value)
          return;
        previousGameplayMode = _currentGameplayMode;
        _currentGameplayMode = value;
        OnGameplayModeChanged?.Invoke(previousGameplayMode, _currentGameplayMode);
      }
    }
    
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

    private void Start() { 
      ChangeGameplayMode(GameplayMode.LOOK_AROUND);
      DependencyManager.Instance.InputHandler.OnPausePressed += OnPausePressedHandler;
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

    public void ChangeGameplayMode(GameplayMode newAction) {
      CurrentGameplayMode = newAction;
      SetInputHandlerFromGameplayMode();
    }

    private void OnPausePressedHandler() {
      if (isGamePaused)
        ResumeGame();
      else
        PauseGame();
    }

    public void PauseGame() {
      isGamePaused = true;
      Time.timeScale = 0;
      DependencyManager.Instance.InputHandler.SwitchContext(InputContext.UI);
      OnGamePaused?.Invoke();
    }

    public void ResumeGame() {
      isGamePaused = false;
      Time.timeScale = 1;
      SetInputHandlerFromGameplayMode();
      OnGameResumed?.Invoke();
    }

    public void SetInputHandlerFromGameplayMode() {
      switch (CurrentGameplayMode) {
        case GameplayMode.LOOK_AROUND:
          DependencyManager.Instance.InputHandler.SwitchContext(InputContext.GAMEPLAY);
          break;
        case GameplayMode.DOCUMENT:
          DependencyManager.Instance.InputHandler.SwitchContext(InputContext.DOCUMENT);
          break;
        case GameplayMode.INTERACT_UI:
          DependencyManager.Instance.InputHandler.SwitchContext(InputContext.UI);
          break;
      }
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
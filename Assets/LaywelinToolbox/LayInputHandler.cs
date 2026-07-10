using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Laywelin {
  
  public enum InputContext {
    GAMEPLAY,
    UI,
    CUTSCENE
  }

  public class InputHandler : MonoBehaviour {
    private InputDevice lastDetectedDevice;
    private GameInputActions gameInputActions;
    [NonSerialized] public InputContext inputContext;
    
    public event Action
      OnPausePressed,
      OnGameplayInteractPressed,
      OnGameplayRotateWorldLeftPressed,
      OnGameplayRotateWorldRightPressed,
      OnUISubmitPressed,
      OnUICancelPressed,
      OnSwitchDevice;
    
    protected void Awake() {
      Cursor.lockState = CursorLockMode.Locked;
      // Cursor.visible = false;

      gameInputActions = new();
      gameInputActions.Global.Enable();
      SwitchContext(InputContext.GAMEPLAY);      
    }

    public void SwitchContext(InputContext context) {
      inputContext = context;

      gameInputActions.Gameplay.Disable();
      gameInputActions.UI.Disable();

      switch (inputContext) {
        case InputContext.GAMEPLAY:
          gameInputActions.Gameplay.Enable();
          break;

        case InputContext.UI:
          gameInputActions.UI.Enable();
          break;

        case InputContext.CUTSCENE:
          break;
      }

      ApplyCursorState();
    }

    private void ApplyCursorState() {
      switch (inputContext) {
        case InputContext.GAMEPLAY:
        case InputContext.CUTSCENE:
        case InputContext.UI:
          Cursor.lockState = CursorLockMode.None;
          Cursor.visible = false;
          break;
      }
    }

    private void Update() {
      if (WasPausePressed())
        OnPausePressed?.Invoke();
      
      if (WasGameplayInteractPressed())
        OnGameplayInteractPressed?.Invoke();

      if (WasGameplayRotateWorldLeftPressed())
        OnGameplayRotateWorldLeftPressed?.Invoke();

      if (WasGameplayRotateWorldRightPressed())
        OnGameplayRotateWorldRightPressed?.Invoke();
      
      if (WasUISubmitPressed())
        OnUISubmitPressed?.Invoke();

      if (WasUICancelPressed())
        OnUICancelPressed?.Invoke();
    }

    // GLOBAL
    
    public bool WasPausePressed() {
      return gameInputActions.Global.Pause.WasPressedThisFrame();
    }
    
    // GAMEPLAY
    
    public Vector2 GetCursorMovementInput() {
      return gameInputActions.Gameplay.MoveCursor.ReadValue<Vector2>();
    }

    public bool WasGameplayInteractPressed() {
      return CanProcessGameplayInput() && gameInputActions.Gameplay.Interact.WasPressedThisFrame();
    }

    public bool WasGameplayRotateWorldLeftPressed() {
      return CanProcessGameplayInput() && gameInputActions.Gameplay.RotateWorldLeft.WasPressedThisFrame();
    }

    public bool WasGameplayRotateWorldRightPressed() {
      return CanProcessGameplayInput() && gameInputActions.Gameplay.RotateWorldRight.WasPressedThisFrame();
    }

    public bool IsGameplayZoomPressed() {
      return CanProcessGameplayInput() && gameInputActions.Gameplay.Zoom.IsPressed();
    }

    public bool WasGameplayZoomReleased() {
      return CanProcessGameplayInput() && gameInputActions.Gameplay.Zoom.WasReleasedThisFrame();
    }

    // UI

    public Vector2 GetUINavigation() {
      if (!CanProcessUIInput())
        return Vector2.zero;

      return gameInputActions.UI.Navigate.ReadValue<Vector2>();
    }

    public bool WasUISubmitPressed() {
      return CanProcessUIInput() && gameInputActions.UI.Submit.WasPressedThisFrame();
    }

    public bool WasUICancelPressed() {
      return CanProcessUIInput() && gameInputActions.UI.Cancel.WasPressedThisFrame();
    }
    
    // "CAN PROCESS" HELPERS

    private bool CanProcessGameplayInput() {
      return Application.isFocused && inputContext == InputContext.GAMEPLAY;
    }

    public bool CanProcessUIInput() {
      return Application.isFocused && inputContext == InputContext.UI;
    }

    private void OnActionChange(object obj, InputActionChange change) {
      if (change == InputActionChange.ActionPerformed) {
        var action = obj as InputAction;
        if (action == null || action.activeControl == null)
          return;

        var device = action.activeControl.device;

        if (device == lastDetectedDevice)
          return;

        bool wasGamepad = lastDetectedDevice is Gamepad;
        bool isGamepad = device is Gamepad;

        lastDetectedDevice = device;
        if (wasGamepad == isGamepad)
          return;

        OnSwitchDevice?.Invoke();
      }
    }

    public bool IsUsingGamepad() {
      return lastDetectedDevice is Gamepad;
    }

    private void OnDestroy() {
      gameInputActions.Dispose();
    }
  }
}


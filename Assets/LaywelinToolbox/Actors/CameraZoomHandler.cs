using System;
using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Laywelin {
  public class CameraZoomHandler : MonoBehaviour {
    [SerializeField] private CinemachineCamera mainCam, dummyCam;
    [SerializeField] private Transform dummyCameraTarget;
    private InputHandler inputHandler;

    private void Start() {
      inputHandler = DependencyManager.Instance.InputHandler;
    }

    private bool proceduralDummyAdjustment = false;

    private void Update() {
      if (inputHandler == null)
        return;

      dummyCam.transform.position = mainCam.transform.position;
      
      bool isZooming = inputHandler.IsGameplayZoomPressed();
      
      mainCam.Priority.Value = isZooming ? 0 : 1;
      dummyCam.Priority.Value = isZooming ? 1 : 0;

      if (inputHandler.WasGameplayZoomReleased()) {
        proceduralDummyAdjustment = true;
        DOVirtual.DelayedCall(0.2f, () => {
          proceduralDummyAdjustment = false;
        }).OnKill(() => {
          proceduralDummyAdjustment = false;
        });
      }
      
      if (!isZooming && !proceduralDummyAdjustment)
        dummyCameraTarget.position = GetCursorFocusPoint();
    }
    
    private Vector3 GetCursorFocusPoint() {
      Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

      RaycastHit[] hits = Physics.RaycastAll(ray);

      foreach (var hit in hits) {
        if (hit.collider.CompareTag("GroundPlane"))
          return hit.point;
      }
      
      return Vector3.zero;
    }
  }
}
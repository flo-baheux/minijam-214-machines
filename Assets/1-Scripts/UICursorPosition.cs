using UnityEngine;
using UnityEngine.InputSystem;

public class CursorPosition : MonoBehaviour {
  private void Awake() {
    Cursor.visible = false;
  }

  private void LateUpdate() {
    Vector3 mousePos = Mouse.current.position.ReadValue();
    mousePos.z = transform.position.z;
    transform.position = mousePos;
  }
}
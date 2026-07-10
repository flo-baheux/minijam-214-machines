using UnityEngine;
using UnityEngine;

public class BillboardEffect : MonoBehaviour {
  [SerializeField] private Camera targetCamera;

  private void LateUpdate() {
    if (targetCamera == null)
      targetCamera = Camera.main;

    if (targetCamera == null)
      return;

    transform.rotation = Quaternion.LookRotation(
      transform.position - targetCamera.transform.position,
      Vector3.up
    );
  }
}
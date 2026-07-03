#nullable enable
using System;
using UnityEngine;

namespace Laywelin {
  public class HitscanWeapon : MonoBehaviour {
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float range = 100f;
    [SerializeField] private int damage = 10;
    [SerializeField] private LayerMask hitMask;


    private void Start() {
      DependencyManager.Instance.InputHandler.OnGameplayInteractPressed += Fire;
    }

    private void Fire() {
      if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, range, hitMask)) {
        if (hit.collider.TryGetComponent(out IDamageable damageable)) {
          damageable.TakeDamage(damage);
        }
      }
    }
  }
}
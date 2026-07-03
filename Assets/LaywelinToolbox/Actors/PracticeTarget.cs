using System;
using DG.Tweening;
using UnityEngine;

namespace Laywelin {
  public class PracticeTarget : MonoBehaviour {
    [SerializeField] private HealthComponent healthComponent;
    [SerializeField] private Renderer mainRenderer;
    [SerializeField] private ParticleSystem explosionParticles;

    [SerializeField] private Color damageColor = Color.red;
    [SerializeField] private float blinkDuration = 0.1f;

    private Material _material;
    private Color _originalColor;
    private Tween _blinkTween;

    private void Awake() {
      if (healthComponent == null)
        healthComponent = GetComponent<HealthComponent>();

      if (mainRenderer == null)
        mainRenderer = GetComponentInChildren<Renderer>();

      _material = mainRenderer.material;
      _originalColor = _material.color;

      healthComponent.OnHealthChanged += OnHealthChangedHandler;
      healthComponent.OnDestroyed += OnDestroyedHandler;
    }

    private void OnDestroy() {
      if (healthComponent != null) {
        healthComponent.OnHealthChanged -= OnHealthChangedHandler;
        healthComponent.OnDestroyed -= OnDestroyedHandler;
      }

      _blinkTween?.Kill();
    }

    private void OnHealthChangedHandler(int hpBefore, int hpAfter) {
      int damageAmount = hpBefore - hpAfter;
      if (damageAmount <= 0)
        return;
      _blinkTween?.Kill();

      _blinkTween = DOTween.Sequence()
        .Append(_material.DOColor(damageColor, blinkDuration * 0.5f))
        .Append(_material.DOColor(_originalColor, blinkDuration * 0.5f))
        .SetLoops(3)
        .SetLink(gameObject);
    }

    private void OnDestroyedHandler() {
      _blinkTween?.Kill();

      if (explosionParticles != null) {
        explosionParticles.transform.parent = null;
        explosionParticles.Play();

        Destroy(
          explosionParticles.gameObject,
          explosionParticles.main.duration + explosionParticles.main.startLifetime.constantMax
        );
      }

      Destroy(gameObject);
    }
  }
}
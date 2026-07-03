using System;
using UnityEngine;

namespace Laywelin {
  public class HealthComponent : MonoBehaviour, IDamageable {
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int startHealth = 100;
    [SerializeField] private bool canTakeDamage = true;
    [SerializeField] private bool canBeHealed = true;

    public event Action<int, int> OnHealthChanged;
    public event Action OnDestroyed;

    public int CurrentHealth { get; private set; }

    public bool IsDead => CurrentHealth <= 0;

    private void Awake() {
      CurrentHealth = Mathf.Clamp(startHealth, 0, maxHealth);
    }

    public void TakeDamage(int damageValue) {
      if (!canTakeDamage || IsDead || damageValue <= 0)
        return;
      
      int previousHealth = CurrentHealth;
      CurrentHealth = Mathf.Clamp(CurrentHealth - damageValue, 0, maxHealth);
      int damageTaken = previousHealth - CurrentHealth;
      if (damageTaken <= 0)
        return;

      OnHealthChanged?.Invoke(previousHealth, CurrentHealth);
      if (CurrentHealth <= 0)
        Die();
    }

    public void Heal(int healingValue) {
      if (!canBeHealed || IsDead || healingValue <= 0)
        return;

      int previousHealth = CurrentHealth;
      CurrentHealth = Mathf.Clamp(CurrentHealth + healingValue, 0, maxHealth);
      int healedAmount = CurrentHealth - previousHealth;

      if (healedAmount <= 0)
        return;
      
      OnHealthChanged?.Invoke(previousHealth, CurrentHealth);
    }

    private void Die() {
      canTakeDamage = false;
      canBeHealed = false;
      OnDestroyed?.Invoke();
    }
  }
}
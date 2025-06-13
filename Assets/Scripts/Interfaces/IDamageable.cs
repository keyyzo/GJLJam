using UnityEngine;

public interface IDamageable<T>
{
    int CurrentHealth { get; }

    void ProcessDamage(T damageTaken);
}

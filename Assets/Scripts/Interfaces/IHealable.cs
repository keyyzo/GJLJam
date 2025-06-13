using UnityEngine;

public interface IHealable<T>
{
    void ProcessHeal(T healAmount);
}

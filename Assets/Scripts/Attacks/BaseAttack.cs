using UnityEngine;

public abstract class BaseAttack : MonoBehaviour, IAttack
{
    [SerializeField] protected int attackDamage = 10;
    public abstract void Attack();
}

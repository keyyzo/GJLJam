using UnityEngine;

public class BaseHealthComponent : MonoBehaviour, IDamageable<int>, IKillable
{
    [Header("Health Settings")]

    [SerializeField] protected int maxHealth = 100;
    [SerializeField] protected int minHealth = 0;
    [SerializeField] protected int currentHealth;

    // private variables

    protected int _defaultMaxHealth = 100;

    public int CurrentHealth => currentHealth;

    private void Start()
    {
        maxHealth = SetDefaultHealth(maxHealth);
        currentHealth = maxHealth;
    }


    protected int SetDefaultHealth(int currentMaxHealth)
    {
        if (currentMaxHealth > 0)
            return currentMaxHealth;
        else
        {
            return _defaultMaxHealth;
        }
    }


    public virtual void ProcessDamage(int damageTaken)
    {
        currentHealth -= damageTaken;

        Debug.Log(gameObject.name + " Current Health Remaining: " + currentHealth);

        if (currentHealth <= minHealth)
        { 
            ProcessKill();
        }
    }

    public virtual void ProcessKill()
    {
        Destroy(this.gameObject);
    }

    
}

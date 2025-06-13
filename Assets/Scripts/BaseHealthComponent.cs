using UnityEngine;

public class BaseHealthComponent : MonoBehaviour, IDamageable<int>, IKillable
{
    [Header("Health Settings")]

    [SerializeField] int maxHealth = 100;
    [SerializeField] int minHealth = 0;
    [SerializeField] int currentHealth;

    // private variables

    protected int _defaultMaxHealth = 100;

    public int CurrentHealth => currentHealth;

    private void Start()
    {
        maxHealth = SetDefaultHealth(maxHealth);
        currentHealth = maxHealth;
    }


    int SetDefaultHealth(int currentMaxHealth)
    {
        if (currentMaxHealth > 0)
            return currentMaxHealth;
        else
        {
            return _defaultMaxHealth;
        }
    }


    public void ProcessDamage(int damageTaken)
    {
        currentHealth -= damageTaken;

        Debug.Log(gameObject.name + " Current Health Remaining: " + currentHealth);

        if (currentHealth <= minHealth)
        { 
            ProcessKill();
        }
    }

    public void ProcessKill()
    {
        Destroy(this.gameObject);
    }

    
}

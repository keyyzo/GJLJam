using System;
using UnityEngine;

public class PlayerHealth : BaseHealthComponent, IHealable<int>
{

    private void Start()
    {
        maxHealth = SetDefaultHealth(maxHealth);
        currentHealth = maxHealth;

        UIManager.Instance.DisplayPlayerHealth(currentHealth, maxHealth);

       
    }

    public void ProcessHeal(int healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth > maxHealth)
        { 
            //currentHealth = Math.Clamp(currentHealth, minHealth, maxHealth);
            currentHealth = maxHealth;
        }

        UIManager.Instance.DisplayPlayerHealth(currentHealth, maxHealth);

        Debug.Log("Player was healed!");
        
    }


    public void UpgradeMaxHealth(int upgradeIncrease)
    {
        if (upgradeIncrease <= 0)
            return;

        maxHealth += upgradeIncrease;
        currentHealth += upgradeIncrease;

        UIManager.Instance.DisplayPlayerHealth(currentHealth, maxHealth);

        Debug.Log("Player Max Health was increased!");

    }

    public override void ProcessDamage(int damageTaken)
    {
        base.ProcessDamage(damageTaken);

        UIManager.Instance.DisplayPlayerHealth(currentHealth, maxHealth);
    }

    public override void ProcessKill()
    {

        GameManager.Instance.OnPlayerDead();

        base.ProcessKill();

        
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

}

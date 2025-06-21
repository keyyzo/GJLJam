using System;
using UnityEngine;

public class PlayerHealth : BaseHealthComponent, IHealable<int>
{
    
    public void ProcessHeal(int healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth > maxHealth)
        { 
            currentHealth = Math.Clamp(currentHealth, minHealth, maxHealth);
        }

        Debug.Log("Player was healed!");
    }


    public void UpgradeMaxHealth(int upgradeIncrease)
    {
        if (upgradeIncrease <= 0)
            return;

        maxHealth += upgradeIncrease;
        currentHealth += upgradeIncrease;

        Debug.Log("Player Max Health was increased!");

    }
   
}

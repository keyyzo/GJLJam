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
    }


    public void UpgradeMaxHealth(int upgradeIncrease)
    {
        if (upgradeIncrease <= 0)
            return;

        maxHealth += upgradeIncrease;
        currentHealth += upgradeIncrease;

    }
   
}

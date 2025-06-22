using System;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Resource Attributes")]

    [SerializeField] int currentResourceAmount = 0;

    [Space(5)]

    [Header("Ammo Attributes")]

    [SerializeField] int maxAmmoAmount = 999;
    [SerializeField] int currentAmmoAmount = 0;


    // public properties

    public int CurrentResourceAmount => currentResourceAmount;
    public int CurrentAmmoAmount => currentAmmoAmount;

    // Cached Components

    PlayerActiveAttack playerActiveAttack;

    private void Awake()
    {
        playerActiveAttack = GetComponentInChildren<PlayerActiveAttack>();
    }

    private void Update()
    {
        UIManager.Instance.SetPlayerStashAmmo(currentAmmoAmount);
        UIManager.Instance.playerResourceAmount = currentResourceAmount;
    }

    #region Resource Methods

    public void ReceiveResource(int amountToReceive)
    {
        if (amountToReceive > 0)
        {
            currentResourceAmount += amountToReceive;

            Debug.Log("Updated total amount of resource: " + currentResourceAmount);
        }
        
    }

    public void SpendResource(int amountToSpend)
    {
        if (currentResourceAmount >= amountToSpend)
        { 
            currentResourceAmount -= amountToSpend;

            Debug.Log("Updated total amount of resource: " + currentResourceAmount);
        }
    }

    #endregion

    #region Ammo Methods

    public void ReceiveAmmo(int ammoAmountToReceive)
    {
        if (ammoAmountToReceive > 0)
        {
            Debug.Log("Amount of ammo picked up: " + ammoAmountToReceive);

            currentAmmoAmount += ammoAmountToReceive;

            if (currentAmmoAmount > maxAmmoAmount)
            { 
                currentAmmoAmount = maxAmmoAmount;
            }

            Debug.Log("Current ammo in inventory: " + currentAmmoAmount);
        }
    }

    public int ReloadAttackAmmo(int currentClipAmount, int maxClipAmount)
    {


        //if (currentAmmoAmount > 0)
        //{
        //    int ammoToReload = Mathf.Min(maxClipAmount - currentClipAmount, currentAmmoAmount);
        //    currentClipAmount += ammoToReload;
        //    currentAmmoAmount -= ammoToReload;
        //}

        //else
        //{
        //    return;
        //}

        int ammoToReload = Mathf.Min(maxClipAmount - currentClipAmount, currentAmmoAmount);
        currentAmmoAmount -= ammoToReload;

        return ammoToReload;


    }

    #endregion


}

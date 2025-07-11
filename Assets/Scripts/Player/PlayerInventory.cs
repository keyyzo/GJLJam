using NUnit.Framework;
using System.Collections.Generic;
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

    [Space(5)]

    [Header("Weapon Attributes")]

    [SerializeField] BaseAttack[] attackSlots = new BaseAttack[2];

    // private variables

    List<BaseAttack> currentlyHeldAttacks = new List<BaseAttack>();

    int currentSlotActive = 10;

    // public properties

    public int CurrentResourceAmount => currentResourceAmount;
    public int CurrentAmmoAmount => currentAmmoAmount;

    public List<BaseAttack> CurrentlyHeldAttacks => currentlyHeldAttacks;


    // Cached Components

    PlayerActiveAttack playerActiveAttack;

    private void Awake()
    {
        playerActiveAttack = GetComponentInChildren<PlayerActiveAttack>();
    }

    private void Start()
    {
        if (playerActiveAttack.CurrentAttack)
        {
            currentlyHeldAttacks.Add(playerActiveAttack.CurrentAttack);
            attackSlots[0] = Instantiate(playerActiveAttack.CurrentAttack);
        }
    }

    private void Update()
    {
        UIManager.Instance.SetPlayerStashAmmo(currentAmmoAmount);
        UIManager.Instance.playerResourceAmount = currentResourceAmount;

        //Debug.Log(attackSlots[0]);
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

    #region Weapon Methods

    public void SwitchWeaponSlot(int slotNum)
    { 
        if(slotNum < 0 || currentSlotActive == slotNum || slotNum >= attackSlots.Length || attackSlots[slotNum ] == null)
            return;

        playerActiveAttack.SwitchAttack(attackSlots[slotNum ]);
        currentSlotActive = slotNum;

    }

    public void PickupWeapon(BaseAttack attackToAdd)
    { 
        if(attackToAdd == null)
            return;

        for (int i = 0; i < attackSlots.Length; i++)
        {
            if (attackSlots[i] == null && attackSlots[i] != attackToAdd)
            {
                attackSlots[i] = Instantiate(attackToAdd);

                if (i == 0 && playerActiveAttack.CurrentAttack == null)
                { 
                    SwitchWeaponSlot(i);
                    //Debug.Log("Initial Gun Swap Initiated");
                }

                break;
            }

            else
            {
                continue;
            }

        }
    }

    #endregion
}

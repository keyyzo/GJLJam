using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] int resourceAmount = 0;


    // public properties

    public int ResourceAmount => resourceAmount;

    // Cached Components

    PlayerActiveAttack playerActiveAttack;

    private void Awake()
    {
        playerActiveAttack = GetComponentInChildren<PlayerActiveAttack>();
    }


    #region Resource Methods

    public void ReceiveResource(int amountToReceive)
    {
        if (amountToReceive > 0)
        {
            resourceAmount += amountToReceive;

            Debug.Log("Updated total amount of resource: " + resourceAmount);
        }
        
    }

    public void SpendResource(int amountToSpend)
    {
        if (resourceAmount >= amountToSpend)
        { 
            resourceAmount -= amountToSpend;

            Debug.Log("Updated total amount of resource: " + resourceAmount);
        }
    }

    #endregion


}

using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
   


    // Cached Components

    PlayerInputs playerInputs;
    PlayerMovement playerMovement;
    PlayerAiming playerAiming;
    PlayerActiveAttack playerActiveAttack;
    PlayerInventory playerInventory;
    PlayerInteractor playerInteractor;
    PlayerHealth playerHealth;


    private void Awake()
    {
        InitializePlayerComponents();
    }

    private void Update()
    {
        if (!GameManager.Instance.isGamePaused)
        {
            playerMovement.HandleMovement(playerInputs.Move);
            playerAiming.ProcessAiming(playerInputs.Aim);
            playerActiveAttack.HandleAttack(playerInputs.Attack);
            playerActiveAttack.ReloadAttack(playerInputs.Reload);
            playerInteractor.SetInteracting(playerInputs.Interact);
        }

        
        
    }

    void InitializePlayerComponents()
    { 
        playerInputs = GetComponent<PlayerInputs>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAiming = GetComponent<PlayerAiming>();
        playerActiveAttack = GetComponentInChildren<PlayerActiveAttack>();
        playerInventory = GetComponent<PlayerInventory>();
        playerInteractor = GetComponent<PlayerInteractor>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    public void ProcessMaxHealthUpgrade(int upgradeAmount, int upgradeCost)
    {
       
        playerHealth.UpgradeMaxHealth(upgradeAmount);
        playerInventory.SpendResource(upgradeCost);

    }

    public void ProcessHealthPurchase(int healAmount, int healCost)
    {
       
        playerHealth.ProcessHeal(healAmount);
        playerInventory.SpendResource(healCost);

    }

    public void ProcessAmmoPurhcase(int ammoAmount, int ammoCost)
    {


        playerInventory.ReceiveAmmo(ammoAmount);
        playerInventory.SpendResource(ammoCost);
    }

    public int GetCurrentResourceAmount()
    { 
        return playerInventory.CurrentResourceAmount;
    }

}

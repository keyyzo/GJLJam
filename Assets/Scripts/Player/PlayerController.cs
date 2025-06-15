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


    private void Awake()
    {
        InitializePlayerComponents();
    }

    private void Update()
    {
        playerMovement.HandleMovement(playerInputs.Move);
        playerAiming.ProcessAiming(playerInputs.Aim);
        playerActiveAttack.HandleAttack(playerInputs.Attack);
        playerInteractor.SetInteracting(playerInputs.Interact);
    }

    void InitializePlayerComponents()
    { 
        playerInputs = GetComponent<PlayerInputs>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAiming = GetComponent<PlayerAiming>();
        playerActiveAttack = GetComponentInChildren<PlayerActiveAttack>();
        playerInventory = GetComponent<PlayerInventory>();
        playerInteractor = GetComponent<PlayerInteractor>();
    }
}

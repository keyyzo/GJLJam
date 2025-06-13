using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
   


    // Cached Components

    PlayerInputs playerInputs;
    PlayerMovement playerMovement;
    PlayerAiming playerAiming;


    private void Awake()
    {
        InitializePlayerComponents();
    }

    private void Update()
    {
        playerMovement.HandleMovement(playerInputs.Move);
        playerAiming.ProcessAiming(playerInputs.Aim);
    }

    void InitializePlayerComponents()
    { 
        playerInputs = GetComponent<PlayerInputs>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAiming = GetComponent<PlayerAiming>();
    }
}

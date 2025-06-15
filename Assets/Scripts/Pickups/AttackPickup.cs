using UnityEngine;

public class AttackPickup : IPickup
{
    [SerializeField] BaseAttack attackToPickup;

    public void ProcessPickup(PlayerInventory playerInventory)
    {
        
    }
}

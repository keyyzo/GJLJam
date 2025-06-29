using Unity.VisualScripting;
using UnityEngine;

public class AttackPickup : MonoBehaviour, IPickup, IKillable
{
    [SerializeField] BaseAttack attackToPickup;

    public void ProcessPickup(PlayerInventory playerInventory)
    {
        playerInventory.PickupWeapon(attackToPickup);

        ProcessKill();
    }

    public void ProcessKill()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Detected by something");

        if (other.TryGetComponent(out PlayerInventory playerInventory))
        {
            
            ProcessPickup(playerInventory);
        }

    }
}

using UnityEngine;

public class AmmoPickup : MonoBehaviour, IPickup, IKillable
{
    [Header("Ammo Pickup Attributes")]

    [SerializeField, Range(5, 50)] int amountOfAmmoToPickup = 5;

    // Below will be used when dropped ammo from enemies / loot is implemented

    [SerializeField] int minRandomAmmoToPickup = 3;
    [SerializeField] int maxRandomAmmoToPickup = 999; // 999 to imitate a max ammo for weapon


    // Cached components

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    public void ProcessPickup(PlayerInventory playerInventory)
    {
        playerInventory?.ReceiveAmmo(amountOfAmmoToPickup);

        ProcessKill();
    }

    public void ProcessKill()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerInventory playerInventory))
        {
            ProcessPickup(playerInventory);
        }
    }
}

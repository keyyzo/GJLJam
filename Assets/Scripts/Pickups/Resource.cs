using UnityEngine;

public class Resource : MonoBehaviour, IPickup, IKillable
{
    [Header("Resource Attributes")]

    [SerializeField] protected int amountOfResource = 50;

    [Space(5)]

    [Header("Rotation Attributes")]

    [SerializeField] protected float rotationSpeed = 5.0f;


    // Cached components

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        RotateResourceOnSpot();
    }

    public void ProcessPickup(PlayerInventory playerInventory)
    {
        playerInventory?.ReceiveResource(amountOfResource);

        ProcessKill();
    }


    public void ProcessKill()
    {
        Destroy(gameObject);
    }

    void RotateResourceOnSpot()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerInventory playerInventory))
        { 
            ProcessPickup(playerInventory);
        }
    }
}

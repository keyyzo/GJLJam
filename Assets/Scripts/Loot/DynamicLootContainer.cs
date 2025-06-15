using UnityEngine;

public class DynamicLootContainer : DestroyableLootContainer, IInteractable
{
    [Header("Interactable References")]

    [SerializeField] GameObject promptedText;

    [Space(5)]

    [Header("Interactable Attributes")]

    [SerializeField, Range(4,7)] int numOfResourceToDropOnInteract = 5;
    [SerializeField] float timeToCompleteInteractLoot = 5.0f;

    float interactTimer = 0.0f;
    bool canInteract = false;
    bool interactComplete = false;


    

    public void ProcessInteract()
    {
        Debug.Log("Attempting to loot...");

        if (canInteract && this.gameObject != null)
        {
            interactTimer += Time.deltaTime;

            float tempPercentageDone = (interactTimer / timeToCompleteInteractLoot) * 100f;

            Debug.Log("Current percentage on interact: " + tempPercentageDone);

            if (interactTimer >= timeToCompleteInteractLoot)
            {
                interactComplete = true;

                Debug.Log("Interact on loot complete!");

                DropLoot();
            }
        }
    }

    public void ProcessInteractPrompt()
    {
        if (canInteract)
        {
            Debug.Log("Player has entered interaction area for loot");
        }
    }

    public void CancelInteractPrompt()
    {
        if (!canInteract)
        {
            Debug.Log("Player has left interaction area for loot");
        }
    }

    public override void DropLoot()
    {
        if (!interactComplete)
        {
            base.DropLoot();
        }

        else
        { 
            InteractCompleteSpawn();
        }

        
    }

    void InteractCompleteSpawn()
    {
        if (resourcePrefab)
        {

            for (int i = 0; i < numOfResourceToDropOnInteract; i++)
            {
                Vector3 tempSpawnPos = GenerateRandomSpawnPosition();

                Resource resourceToSpawn = Instantiate(resourcePrefab, tempSpawnPos, Quaternion.identity).GetComponent<Resource>();


            }
        }

        ProcessKill();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BaseAttack>() || other.GetComponent<Projectile>())
        {
            DropLoot();
        }

        if (other.CompareTag("Player"))
        { 
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class DynamicLootContainer : DestroyableLootContainer, IInteractable
{
    [Header("Interactable References")]

    [SerializeField] GameObject promptedText;
    [SerializeField] GameObject progressBarCanvas;
    [SerializeField] Image progressSlider;

    [Space(5)]

    [Header("Interactable Attributes")]

    [SerializeField, Range(4,7)] int numOfResourceToDropOnInteract = 5;
    [SerializeField] float timeToCompleteInteractLoot = 5.0f;

    float interactTimer = 0.0f;
    bool canInteract = false;
    bool interactComplete = false;


    private void Start()
    {
        promptedText.SetActive(false);
        progressSlider.fillAmount = 0.0f;
        progressBarCanvas.SetActive(false);
        
    }

    private void Update()
    {
        PointPromptTextToCamera();
    }


    public void ProcessInteract()
    {
        Debug.Log("Attempting to loot...");

        if (canInteract && (UnityEngine.Object)this != null)
        {
            if (!progressBarCanvas.activeSelf)
            {
                progressBarCanvas.SetActive(true);
            }
            

            interactTimer += Time.deltaTime;

            float tempPercentageDone = (interactTimer / timeToCompleteInteractLoot) * 100f;

            progressSlider.fillAmount = tempPercentageDone / 100f;

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
            promptedText.SetActive(true);
            Debug.Log("Player has entered interaction area for loot");
        }
    }

    public void CancelInteractPrompt()
    {
        if (!canInteract)
        {
            promptedText.SetActive(false);
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

                canInteract = false;
                
            }
        }

        ProcessKill();
    }

    void PointPromptTextToCamera()
    {
        if (promptedText.activeSelf)
        {
            promptedText.gameObject.transform.forward = Camera.main.transform.forward;
        }

        if (progressBarCanvas.activeSelf)
        {
            progressBarCanvas.gameObject.transform.forward = Camera.main.transform.forward;
        }

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

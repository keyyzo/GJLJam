using UnityEngine;

public class ResourceConverter : MonoBehaviour, IInteractable
{
    [Header("Interactable References")]

    [SerializeField] GameObject promptedText;
    [SerializeField] GameObject converterMenu;

    bool canInteract = false;
    bool isMenuActive = false;



    private void Awake()
    {
        promptedText.SetActive(false); 
        if(converterMenu)
            converterMenu.SetActive(false);
    }


    private void Update()
    {
        PointPromptTextToCamera();
    }

    public void ProcessInteract()
    {
        if (canInteract && !isMenuActive)
        { 
            canInteract = false;
            isMenuActive = true;

            converterMenu.SetActive(true);
        }
    }

    public void ProcessInteractPrompt()
    {
        if (canInteract)
        {
            promptedText.SetActive(true);
            Debug.Log("Player has entered interaction area for converting");
        }
    }

    public void CancelInteractPrompt()
    {
        if (!canInteract)
        {
            promptedText.SetActive(false);

            if (converterMenu.activeSelf)
            { 
                converterMenu.SetActive(false);
                isMenuActive = false;
            }

            Debug.Log("Player has entered interaction area for converting");
        }
    }


    void PointPromptTextToCamera()
    {
        if (promptedText.activeSelf)
        {
            promptedText.gameObject.transform.forward = Camera.main.transform.forward;
        }

    }


    private void OnTriggerEnter(Collider other)
    {

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

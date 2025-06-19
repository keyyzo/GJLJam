using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ResourceConverter : MonoBehaviour, IInteractable
{
    [System.Serializable]
    public class ConversionItem
    { 
        public string itemName;
        public GameObject itemObject;
        public Button itemButton;
    }





    [Header("Interactable References")]

    [SerializeField] GameObject promptedText;
    [SerializeField] GameObject converterMenu;

    [Space(5)]

    [Header("Conversion Item References")]

    [SerializeField] GameObject healthDropItem;
    [SerializeField] GameObject ammoDropItem;
    [SerializeField] GameObject maxHealthIncreaseItem;

    [Space(5)]

    [Header("Conversion Item Costs and Received Amount")]

    [SerializeField] int healthCost = 250;
    [SerializeField] int healthToReceive = 25;

    [Space(3)]

    [SerializeField] int ammoCost = 500;
    [SerializeField] int ammoToReceive = 50;

    [Space(3)]

    [SerializeField] int maxHealthIncreaseCost = 3000;
    [SerializeField] int maxHealthIncreaseToReceive = 20;

    // Private variables

    bool canInteract = false;
    bool isMenuActive = false;

    // Cached components

    PlayerController playerObject;
    ConversionItem activeItem;
    
    // Lists

    List<ConversionItem> conversionItems = new List<ConversionItem>();

    private void Awake()
    {
        promptedText.SetActive(false); 
        if(converterMenu)
            converterMenu.SetActive(false);
    }

    void Start()
    { 
        InitializeConversionItems();
    }

    private void Update()
    {
        PointPromptTextToCamera();
    }

    #region Prompt Functions

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

    #endregion


    void InitializeConversionItems()
    { 
        ConversionItem healthItem = new ConversionItem { itemName = "HealthItem", itemObject = healthDropItem, itemButton = healthDropItem.GetComponentInChildren<Button>() };

        conversionItems.Add(healthItem);
    }

    

    void OnHealthItemAdd()
    {
        playerObject.ProcessHealthPurchase(healthToReceive, healthCost);
    }

    void OnMaxHealthIncrease()
    {
        playerObject.ProcessMaxHealthUpgrade(maxHealthIncreaseToReceive, maxHealthIncreaseCost);
    }

    void OnAmmoItemAdd()
    { 
        playerObject.ProcessAmmoPurhcase(ammoToReceive, ammoCost);
    }



    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player") && other.GetComponent<PlayerController>())
        {
            canInteract = true;

            if (playerObject == null)
            {
                playerObject = other.GetComponent<PlayerController>();
            }
            
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<PlayerController>())
        {
            canInteract = false;

            if (playerObject != null)
            {
                playerObject = null;
            }
        }
    }

}

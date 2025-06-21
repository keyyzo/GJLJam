using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class ResourceConverter : MonoBehaviour, IInteractable
{
    //[System.Serializable]
    //public class ConversionItem
    //{ 
    //    public string itemName;
    //    public GameObject itemObject;
    //    public Button itemButton;
    //}





    [Header("Interactable References")]

    [SerializeField] GameObject promptedText;
    [SerializeField] GameObject converterMenu;

    [Space(5)]

    [Header("Conversion Item References")]

    [SerializeField] ConversionItem healthDropItem;
    [SerializeField] ConversionItem ammoDropItem;
    [SerializeField] ConversionItem maxHealthIncreaseItem;

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
    bool isConversionItemActive = false;

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
        SetConversionItemActive();
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
        conversionItems.Add(healthDropItem);
        //conversionItems.Add(ammoDropItem);
        //conversionItems.Add(maxHealthIncreaseItem);
    }


    void SetActiveConversionItem()
    {
        if (!isConversionItemActive && activeItem == null)
        {
            foreach (var item in conversionItems)
            {
                if (item.IsPointerOverUI)
                {
                    activeItem = item;
                    Debug.Log("Active Conversion Item Set");
                    isConversionItemActive = true;
                }

                else
                {
                    Debug.Log("Active Conversion Item Removed");
                    isConversionItemActive = false;
                }
            }
        }

        else
        {
            activeItem.ItemButton.onClick.RemoveAllListeners();

            if (activeItem == healthDropItem)
            {
                Debug.Log("Healing Item Acquired");
                activeItem.ItemButton.onClick.AddListener(OnHealthItemAdd);
            }

            else if (activeItem == ammoDropItem)
            {
                Debug.Log("Ammo Item Acquired");
                activeItem.ItemButton.onClick.AddListener(OnAmmoItemAdd);
            }

            else if (activeItem == maxHealthIncreaseItem)
            {
                Debug.Log("Max Healing Increase Item Acquired");
                activeItem.ItemButton.onClick.AddListener(OnMaxHealthIncrease);
            }
        }
    }

    void SetConversionItemActive()
    {
        foreach (var item in conversionItems)
        {
            if (item.IsPointerOverUI && activeItem == null)
            {
                activeItem = item;
                isConversionItemActive = true;
                Debug.Log("Active Conversion Item Set");
            }

            else
            {
                activeItem?.ItemButton.onClick.RemoveAllListeners();
                activeItem = null;
                isConversionItemActive = false;
                Debug.Log("Active Conversion Item Removed");
            }
        }

        if (activeItem != null)
        {
            activeItem.ItemButton.onClick.RemoveAllListeners();

            if (activeItem == healthDropItem)
            {
                Debug.Log("Healing Item Set as Active Item");
                activeItem.ItemButton.onClick.AddListener(OnHealthItemAdd);
            }

            else if (activeItem == ammoDropItem)
            {
                Debug.Log("Ammo Item Set as Active Item");
                activeItem.ItemButton.onClick.AddListener(OnAmmoItemAdd);
            }

            else if (activeItem == maxHealthIncreaseItem)
            {
                Debug.Log("Max Health Increase Item Set as Active Item");
                activeItem.ItemButton.onClick.AddListener(OnMaxHealthIncrease);
            }
        }

        //else
        //{
        //    Debug.Log("Removed listeners on Active Item");
        //    activeItem.ItemButton.onClick.RemoveAllListeners();
        //}
    }

    


    void OnHealthItemAdd()
    {
        if(playerObject.GetCurrentResourceAmount() >= healthCost)
            playerObject.ProcessHealthPurchase(healthToReceive, healthCost);

    }

    void OnMaxHealthIncrease()
    {
        if(playerObject.GetCurrentResourceAmount() >= maxHealthIncreaseCost)
            playerObject.ProcessMaxHealthUpgrade(maxHealthIncreaseToReceive, maxHealthIncreaseCost);
    }

    void OnAmmoItemAdd()
    { 
        if(playerObject.GetCurrentResourceAmount() >= ammoCost)
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


    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    if (eventData.pointerEnter.transform.gameObject == conversionItems[0].itemObject)
    //    {
    //        activeItem = conversionItems[0];
    //        activeItem.itemButton.onClick.AddListener(OnHealthItemAdd);
    //        Debug.Log("Button Ready!");
    //    }
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    activeItem = null;
    //}
}

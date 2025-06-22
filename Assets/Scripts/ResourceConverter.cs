using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using TMPro;

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

    [SerializeField] TMP_Text resourceAmountText;

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
    bool canBuy = true;

    // Cached components

    PlayerController playerObject;
    ConversionItem activeItem;
    
    // Lists

    [SerializeField] List<ConversionItem> conversionItems = new List<ConversionItem>();

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
        SetCurrentResourceAmountUI();

        if (isMenuActive && (GameManager.Instance.isGamePaused || GameManager.Instance.hasPlayerDied))
        {
            converterMenu.SetActive(false);
        }

        else if (isMenuActive && !GameManager.Instance.isGamePaused)
        {
            converterMenu.SetActive(true);
        }
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
        string healthString = "Heals instantly by: " + healthToReceive.ToString();
        string ammoString = "Increases ammo by: " + ammoToReceive.ToString();
        string maxHealthString = "Increases max health by: " + maxHealthIncreaseToReceive.ToString();



        healthDropItem.SetItemUI(healthCost, healthToReceive, healthString);
        ammoDropItem.SetItemUI(ammoCost, ammoToReceive, ammoString);
        maxHealthIncreaseItem.SetItemUI(maxHealthIncreaseCost, maxHealthIncreaseToReceive, maxHealthString);

        conversionItems.Add(healthDropItem);
        conversionItems.Add(ammoDropItem);
        conversionItems.Add(maxHealthIncreaseItem);
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
    } // NOT USED

    void SetConversionItemActive()
    {
        foreach (ConversionItem item in conversionItems)
        {
            if (item.IsPointerOverUI)
            {

                activeItem = item;
                isConversionItemActive = true;
                break;
                //Debug.Log("Active Conversion Item Set");
            }

            else
            {
                activeItem?.ItemButton.onClick.RemoveAllListeners();
                activeItem = null;
                isConversionItemActive = false;
                //Debug.Log("Active Conversion Item Removed");
            }


        }

        //for (int i = 0; i < conversionItems.Count; i++)
        //{
        //    if (conversionItems[i].IsPointerOverUI)
        //    {
        //        activeItem = conversionItems[i];
        //    }

        //    else
        //    {
        //        activeItem?.ItemButton.onClick.RemoveAllListeners();
        //        activeItem = null;
        //    }
        //}

        if (activeItem != null)
        {
            //activeItem.ItemButton.onClick.RemoveAllListeners();
            //activeItem.ItemButton.interactable = true;

            if (activeItem == healthDropItem)
            {
                //Debug.Log("Healing Item Set as Active Item");
                activeItem.ItemButton.onClick.AddListener(OnHealthItemAdd);

                
            }

            else if (activeItem == ammoDropItem)
            {
                //Debug.Log("Ammo Item Set as Active Item");
                activeItem.ItemButton.onClick.AddListener(OnAmmoItemAdd);

                
            }

            else if (activeItem == maxHealthIncreaseItem)
            {
                //Debug.Log("Max Health Increase Item Set as Active Item");
                activeItem.ItemButton.onClick.AddListener(OnMaxHealthIncrease);

                
            }
        }

        //else
        //{
        //    Debug.Log("Removed listeners on Active Item");
        //    activeItem.ItemButton.onClick.RemoveAllListeners();
        //}
    }

    void SetCurrentResourceAmountUI()
    {
        if (isMenuActive)
        {
            int amountToShow = (int)(playerObject?.GetCurrentResourceAmount());

            resourceAmountText.text = "Resource remaining to spend: " + amountToShow.ToString();
        }
    }


    void OnHealthItemAdd()
    {
        if (canBuy)
        {
            Debug.Log("Health Item Called");
            if (playerObject.GetCurrentResourceAmount() >= healthCost)
                playerObject.ProcessHealthPurchase(healthToReceive, healthCost);
        }

        PurchaseCooldown();
    }

    void OnMaxHealthIncrease()
    {
        if (canBuy)
        {
            if (playerObject.GetCurrentResourceAmount() >= maxHealthIncreaseCost)
                playerObject.ProcessMaxHealthUpgrade(maxHealthIncreaseToReceive, maxHealthIncreaseCost);
        }

        PurchaseCooldown();

    }

    void OnAmmoItemAdd()
    {
        if (canBuy)
        {
            if (playerObject.GetCurrentResourceAmount() >= ammoCost)
                playerObject.ProcessAmmoPurhcase(ammoToReceive, ammoCost);
        }

        PurchaseCooldown();
    }

    void PurchaseCooldown()
    {
        canBuy = false;

        foreach (var item in conversionItems)
        { 
            item.ItemButton.interactable = false;
        }

        StartCoroutine(PurchaseCooldownRoutine());
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


    IEnumerator PurchaseCooldownRoutine()
    {
        yield return new WaitForSeconds(1f);

        foreach (var item in conversionItems)
        {
            item.ItemButton.interactable = true;
        }

        canBuy = true;
    }
}

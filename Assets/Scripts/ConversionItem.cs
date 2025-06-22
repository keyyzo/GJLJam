using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class ConversionItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("References")]

    [SerializeField] TMP_Text itemName;
    [SerializeField] TMP_Text itemDescription;
    [SerializeField] TMP_Text itemCost;
    [SerializeField] TMP_Text itemAmount;

    // private variables

    bool isPointerOverUI = false;

    // public properties

    public bool IsPointerOverUI => isPointerOverUI;

    public Button ItemButton => itemButton;

    public TMP_Text ItemName => itemName;

    // Cached components

    Button itemButton;

    private void Start()
    {
        itemButton = GetComponentInChildren<Button>();
    }


    public void SetItemUI(int priceAmount, int itemAmountReceived, string itemAmountReivedText)
    { 
        itemCost.text = "Cost: " + priceAmount.ToString();
        itemAmount.text = itemAmountReivedText;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOverUI = true;
        //Debug.Log("Mouse is hovering over " + itemName);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOverUI = false;
        //Debug.Log("Mouse has stopped hovering over " + itemName);
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        itemButton.interactable = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        itemButton.interactable = false;
    }
}

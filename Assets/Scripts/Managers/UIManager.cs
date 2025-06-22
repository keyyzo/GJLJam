using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Player Health References")]

    public TMP_Text playerHealthText;
    public Image playerHealthSlider;

    public int playerCurrentHealth;
    public int playerMaxHealth;



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

   

    public void DisplayPlayerHealth(int tempCurrentHealth, int tempMaxHealth)
    {
        float tempCurrentHealthVal = (float)tempCurrentHealth / (float)tempMaxHealth;

        playerHealthSlider.fillAmount = tempCurrentHealthVal;

        playerHealthText.text = tempCurrentHealth.ToString() + " / " + tempMaxHealth;
    }
}

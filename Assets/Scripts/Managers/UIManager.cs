using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Player Health References")]

    public TMP_Text playerHealthText;
    public Image playerHealthSlider;

    [Space(5)]

    [Header("Player Ammo References")]

    public TMP_Text playerAmmoText;
    public bool isRangeWeaponActive = false;

    [Space(5)]

    [Header("Player Resource References")]

    public TMP_Text playerResourceText;
    public int playerResourceAmount;

    // private variables

    int playerClipAmmo = 0;
    int playerStashAmmo = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        UpdateAmmoText();
        UpdateResourceText();
    }

    public void DisplayPlayerHealth(int tempCurrentHealth, int tempMaxHealth)
    {
        float tempCurrentHealthVal = (float)tempCurrentHealth / (float)tempMaxHealth;

        playerHealthSlider.fillAmount = tempCurrentHealthVal;

        playerHealthText.text = tempCurrentHealth.ToString() + " / " + tempMaxHealth;
    }

    public void SetPlayerStashAmmo(int currentStashAmmo)
    {
        playerStashAmmo = currentStashAmmo;
    }

    public void SetPlayerClipAmmo(int currentClipAmmo)
    {
        playerClipAmmo = currentClipAmmo;
    }

    void UpdateAmmoText()
    {
        if (isRangeWeaponActive)
        {
            playerAmmoText.text = "Ammo: " + playerClipAmmo + " / " + playerStashAmmo;
        }

        else
        {
            playerAmmoText.text = "Unlimited";

        }
    }

    void UpdateResourceText()
    {
        playerResourceText.text = "Resource Collected: " + playerResourceAmount;
    }
}

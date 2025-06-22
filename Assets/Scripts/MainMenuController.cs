using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Main Menu References")]

    [SerializeField] GameObject mainMenuObj;
    [SerializeField] GameObject howToPlayObj;

    [SerializeField] GameObject backButtonObj;


    // private variables

    bool isHowToPlaySelected = false;
    bool isCreditsSelected = false;
    bool isSettingsSelected = false;

    private void Start()
    {
        mainMenuObj.SetActive(true);
        howToPlayObj.SetActive(false);
    }

    public void OnGameStart()
    {
        int gameScene = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(gameScene);
    }

    public void OnDisplayHowToPlayer()
    { 
        mainMenuObj?.SetActive(false);
        howToPlayObj?.SetActive(true);

        isHowToPlaySelected = true;
    }

    public void OnDisplayCredits()
    { 
        
        isCreditsSelected = true;
    }

    public void OnDisplaySettings() 
    {
        
        isSettingsSelected = true;
    }

    public void OnExitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif

        Application.Quit();
    }

    public void OnReturnToMainMenu()
    {
        if (isHowToPlaySelected)
        {
            howToPlayObj?.SetActive(false);
            isHowToPlaySelected = false;
        }

        if (isCreditsSelected)
        { 
            
            isCreditsSelected = false;
        }

        if (isSettingsSelected)
        { 
            
            isSettingsSelected = false;
        }

        mainMenuObj?.SetActive(true);
        
    }
}

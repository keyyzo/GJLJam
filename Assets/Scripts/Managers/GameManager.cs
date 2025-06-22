using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Global References")]

    public GameObject player;
    public PlayerController playerController;


    [Space(5)]

    [Header("Game States")]

    public bool isGamePaused = false;
    public bool hasPlayerDied = false;
    public bool isPlayerUsingConverter = false;

    // Constant values

    const string PLAYER_TAG_STRING = "Player";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        player = GameObject.FindWithTag(PLAYER_TAG_STRING);
        playerController = player.GetComponent<PlayerController>();

    }

    public void OnGamePaused()
    { 
        isGamePaused = true;
        Time.timeScale = 0.0f;
    }

    public void OnGameUnpaused()
    {
        isGamePaused = false;
        Time.timeScale = 1.0f;
    }

    public void OnPlayerDead()
    { 
        hasPlayerDied = true;

        StartCoroutine(OnPlayerDeadRoutine());
    }

    IEnumerator OnPlayerDeadRoutine()
    {
        while (Time.timeScale > 0.2f)
        {
            //Time.timeScale = Mathf.Lerp(1.0f, 0.2f, Time.unscaledDeltaTime);
            Time.timeScale -= Time.unscaledDeltaTime;
        }

        Time.timeScale = 0.2f;

        yield return null;
    }
}

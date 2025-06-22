using System.Collections;
using TMPro;
using UnityEngine;

public class RoundSystem : MonoBehaviour
{
    [Header("UI References")]

    public TMP_Text roundTimerText;
    public TMP_Text roundNumberText;


    [Space(5)]

    [Header("Round Count Attributes")]

    public int roundNumber = 1;
    public float roundActiveLength = 90.0f;
    public bool isRoundActive = false;

    [Space(5)]

    [Header("Round End Attributes")]

    public float roundEndLength = 45.0f;
    public bool hasRoundEnded = false;

    [Space(5)]

    [Header("Attributes to Increase Per Round")]

    public int enemyDamageIncrease = 10;
    public float minEnemySpawnRateChange = 0.1f;
    public float maxEnemySpawnRateChange = 0.5f;

    [Space(5)]

    [Header("Other Attributes")]

    [SerializeField] float quickBreakRoutineLength = 2f;

    // private variables

    float roundActiveTimer = 0.0f;
    float roundEndedTimer = 0.0f;

    int currentEnemyDamageIncrease = 0;

    string roundActiveTimerText = "Time left in round: ";
    string roundEndTimerText = "Time left before round begins: ";
    string roundNumberPreText = "Current Round: ";
    string roundOverText = "Round Over!";
    string roundBeginText = "Round Starting!";

    // Cached components

    EnemySpawner enemySpawner;

    private void Awake()
    {
        enemySpawner = GetComponent<EnemySpawner>();
    }

    private void Start()
    {
        StartActiveRound();
    }

    private void Update()
    {
        if (!GameManager.Instance.isGamePaused)
        {
            ProcessActiveRound();
            ProcessEndRound();
        }
        
    }

    void ProcessActiveRound()
    {
        if (isRoundActive && !hasRoundEnded)
        { 
            enemySpawner.SpawnNextEnemy(currentEnemyDamageIncrease);

            roundActiveTimer -= Time.deltaTime;
            roundTimerText.text = roundActiveTimerText + roundActiveTimer.ToString("F2");


            if (roundActiveTimer <= 0.0f)
            { 
                roundActiveTimer = 0.0f;
                roundTimerText.text = roundActiveTimerText + roundActiveTimer.ToString("F2");
                EndActiveRound();
            }
        }
    }

    void ProcessEndRound()
    {
        if (!isRoundActive && hasRoundEnded)
        { 
            roundEndedTimer -= Time.deltaTime;
            roundTimerText.text = roundEndTimerText + roundEndedTimer.ToString("F2");

            if (roundEndedTimer <= 0.0f)
            { 
                roundEndedTimer = 0.0f;
                roundTimerText.text = roundEndTimerText + roundEndedTimer.ToString("F2");
                FinishedEndRound();
            }
        }
    }

    void StartActiveRound()
    {
        if (!isRoundActive)
        {
            roundActiveTimer = roundActiveLength;
            roundTimerText.text = roundActiveTimerText + roundActiveTimer.ToString("F2");
            roundNumberText.text = roundNumberPreText + roundNumber.ToString();

            isRoundActive = true;
        }
    }


    void StartEndRound()
    { 
        hasRoundEnded = true;

        roundEndedTimer = roundEndLength;
        roundTimerText.text = roundEndTimerText + roundEndedTimer.ToString("F2");


    }


    void SetupNextRound()
    {
        currentEnemyDamageIncrease += enemyDamageIncrease;
        roundNumber += 1;

        roundNumberText.text = roundNumberPreText + roundNumber.ToString();

        enemySpawner.MinNextSpawnTime -= minEnemySpawnRateChange;

        if (enemySpawner.MinNextSpawnTime < 0.1f)
        {
            enemySpawner.MinNextSpawnTime = 0.1f;
        }

        enemySpawner.MaxNextSpawnTime -= maxEnemySpawnRateChange;

        if (enemySpawner.MaxNextSpawnTime < 2.5f)
        {
            enemySpawner.MaxNextSpawnTime = 2.5f;
        }




    }

    void EndActiveRound()
    {
        roundTimerText.text = roundOverText;

        StartCoroutine(EndActiveRoundRoutine());
    }

    void FinishedEndRound()
    {
        roundTimerText.text = roundBeginText;
        StartCoroutine(FinishedEndRoundRoutine());
    }


    IEnumerator EndActiveRoundRoutine()
    { 
        isRoundActive = false;

        //StartCoroutine(QuickBreakRoutine());

        yield return new WaitForSeconds(quickBreakRoutineLength);

        StartEndRound();
    }

    IEnumerator FinishedEndRoundRoutine()
    {
        hasRoundEnded = false;


        //StartCoroutine (QuickBreakRoutine());

        yield return new WaitForSeconds(quickBreakRoutineLength);
        SetupNextRound();

        StartActiveRound();
    }

    IEnumerator QuickBreakRoutine()
    {
        yield return new WaitForSeconds(quickBreakRoutineLength);
    }
}

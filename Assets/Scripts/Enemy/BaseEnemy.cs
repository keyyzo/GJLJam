using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour, ISpawnable
{
    [Header("Enemy Attributes")]

    [SerializeField] protected int enemyDamage = 20;

    [Space(5)]

    [Header("Spawnable Attributes")]

    [SerializeField] GameObject resourcePrefab;
    [SerializeField, Range(0, 2)] protected int minNumOfResourceToDropOnDestroy = 3;
    [SerializeField, Range(3, 5)] protected int maxNumOfResourceToDropOnDestroy = 3;
    [SerializeField] protected float minRandomSpawnOffset = -5f;
    [SerializeField] protected float maxRandomSpawnOffset = 5f;

    protected Vector3 randomSpawnPosition;


    public int EnemyDamage => enemyDamage;

    const string PLAYER_STRING = "Player";

    // Cached components

    NavMeshAgent agent;
    PlayerController playerObject;
    BaseHealthComponent enemyHealthComponent;
    BaseDropper baseDropper;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyHealthComponent = GetComponent<BaseHealthComponent>();
        baseDropper = GetComponent<BaseDropper>();
    }

    private void Start()
    {
        playerObject = FindFirstObjectByType<PlayerController>();
    }

    private void Update()
    {
        BasicMoveAgentTowardsPlayer();
    }

    void BasicMoveAgentTowardsPlayer()
    { 
        if(!playerObject)
            return;

        agent.SetDestination(playerObject.transform.position);
        
    }

    public void IncreaseEnemyDamage(int newDamageVal)
    { 
        enemyDamage += newDamageVal;
    }

    //public void ProcessSpawn()
    //{
    //    if (resourcePrefab)
    //    {
    //        int randomAmountToSpawn = Random.Range(minNumOfResourceToDropOnDestroy, maxNumOfResourceToDropOnDestroy);

    //        for (int i = 0; i < randomAmountToSpawn; i++)
    //        {
    //            Vector3 tempSpawnPos = GenerateRandomSpawnPosition();

    //            Resource resourceToSpawn = Instantiate(resourcePrefab, tempSpawnPos, Quaternion.identity).GetComponent<Resource>();


    //        }
    //    }


    //}

    public void ProcessSpawn()
    { 
        baseDropper.ProcessSpawn();
    }

    protected Vector3 GenerateRandomSpawnPosition()
    {

        float randomXPos = Random.Range(minRandomSpawnOffset, maxRandomSpawnOffset);
        float randomZPos = Random.Range(minRandomSpawnOffset, maxRandomSpawnOffset);

        Vector3 newSpawnPosition = new Vector3(transform.position.x + randomXPos, transform.position.y, transform.position.z + randomZPos);

        return newSpawnPosition;
    }


    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag(PLAYER_STRING)))
        {
            Debug.Log("Trigger collided with player");

            if(other.TryGetComponent(out BaseHealthComponent playerHealth))
            {
                playerHealth?.ProcessDamage(enemyDamage);
                enemyHealthComponent.ProcessKill();
            }
        }
    }

    
}

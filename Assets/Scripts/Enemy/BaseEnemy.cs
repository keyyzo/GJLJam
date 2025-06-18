using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    [Header("Enemy Attributes")]

    [SerializeField] int enemyDamage = 20;


    const string PLAYER_STRING = "Player";

    // Cached components

    NavMeshAgent agent;
    PlayerController playerObject;
    BaseHealthComponent enemyHealthComponent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyHealthComponent = GetComponent<BaseHealthComponent>();
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

using System.Collections.Generic;
using UnityEngine;

public class DestroyableLootContainer : BaseLootContainer, ISpawnable, IKillable
{
    [Header("References")]

    [SerializeField] protected GameObject resourcePrefab;

    [Space(5)]

    [Header("Loot Drop Attributes")]

    [SerializeField, Range(1,5)] protected int numOfResourceToDropOnDestroy = 3;
    [SerializeField] protected float minRandomSpawnOffset = -5f;
    [SerializeField] protected float maxRandomSpawnOffset = 5f;

    protected Vector3 randomSpawnPosition;




    public override void DropLoot()
    {
        ProcessSpawn();
    }


    public void ProcessSpawn()
    {
        if (resourcePrefab)
        {

            for (int i = 0; i < numOfResourceToDropOnDestroy; i++)
            {
                Vector3 tempSpawnPos = GenerateRandomSpawnPosition();

                Resource resourceToSpawn = Instantiate(resourcePrefab, tempSpawnPos, Quaternion.identity).GetComponent<Resource>();


            }
        }

        ProcessKill();
    }

    public virtual void ProcessKill()
    {
        Destroy(this.gameObject);
    }

    protected Vector3 GenerateRandomSpawnPosition()
    { 

        float randomXPos = Random.Range(minRandomSpawnOffset, maxRandomSpawnOffset);
        float randomZPos = Random.Range(minRandomSpawnOffset,maxRandomSpawnOffset);

        Vector3 newSpawnPosition = new Vector3(transform.position.x + randomXPos, transform.position.y, transform.position.z + randomZPos);

        return newSpawnPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BaseAttack>() || other.GetComponent<Projectile>())
        {
            DropLoot();
        }
    }
}

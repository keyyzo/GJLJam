using System.Collections.Generic;
using UnityEngine;

public class BaseDropper : MonoBehaviour, ISpawnable
{
    [Header("Prefab References")]

    [SerializeField] List<SpawnableItem> droppablePrefabs;

    public void ProcessSpawn()
    {
        if(droppablePrefabs.Count <= 0)
            return;

        for (int i = 0; i < droppablePrefabs.Count; i++)
        {
            float tempChanceToSpawnItem = Random.Range(0.00f, 100.0f);

            if (tempChanceToSpawnItem <= droppablePrefabs[i].ChanceToSpawn)
            {
                int randomAmountToSpawn = Random.Range(droppablePrefabs[i].MinNumToSpawn, droppablePrefabs[i].MaxNumToSpawn);

                for (int j = 0; j < randomAmountToSpawn; j++)
                {
                    Vector3 tempSpawnPos = GenerateRandomSpawnPosition(droppablePrefabs[i].MinSpawnOffset, droppablePrefabs[i].MaxSpawnOffset);

                    Instantiate(droppablePrefabs[i].ItemPrefab, tempSpawnPos, Quaternion.identity);
                }

                
            }
        }
    }

    

    /// <summary>
    /// Sets the spawning droppable to a random nearby location from the parent object
    /// </summary>
    /// <param name="minOffset">Minimum offset from the original position</param>
    /// <param name="maxOffset">Maximum offset from the original position</param>
    /// <returns></returns>
    /// 

    // Will eventually be refactored to use an animation alongside the spawning in of an item
    protected Vector3 GenerateRandomSpawnPosition(float minOffset, float maxOffset)
    {

        float randomXPos = Random.Range(minOffset, maxOffset);
        float randomZPos = Random.Range(minOffset, maxOffset);

        Vector3 newSpawnPosition = new Vector3(transform.position.x + randomXPos, transform.position.y, transform.position.z + randomZPos);

        return newSpawnPosition;
    }
}

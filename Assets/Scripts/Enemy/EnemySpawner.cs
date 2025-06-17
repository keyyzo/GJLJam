using UnityEngine;
using UnityEngine.Splines;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]


    [SerializeField] GameObject enemyPrefab;
    [SerializeField] SplineContainer splineContainer;

    [Space(5)]

    [Header("Spawn Attributes")]

    [SerializeField] float minNextSpawnTime = 3.0f;
    [SerializeField] float maxNextSpawnTime = 8.0f;

    float spawnTimer = 0.0f;
    float nextRandomSpawnTime;


    private void Update()
    {
        SpawnNextEnemy();
    }

    void SpawnEnemyOnSpline()
    { 
        if(!enemyPrefab || !splineContainer)
            return;

        float randomPos = Random.Range(0f, 1f);

        Vector3 spawnPosition =  splineContainer.EvaluatePosition(randomPos);

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        nextRandomSpawnTime = Random.Range(minNextSpawnTime, maxNextSpawnTime);
    }

    void SpawnNextEnemy()
    {
        if (spawnTimer < nextRandomSpawnTime)
        {
            spawnTimer += Time.deltaTime;
        }

        else
        { 
            SpawnEnemyOnSpline();
            spawnTimer = 0.0f;
        }


    }

}

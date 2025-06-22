using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Splines;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]


    [SerializeField] GameObject enemyPrefab;
    [SerializeField] SplineContainer splineContainer;

    [Space(5)]

    [Header("Spawn Attributes")]

    [Range(0.1f,10.0f)]
    public float MinNextSpawnTime = 3.0f;
    public float MaxNextSpawnTime = 8.0f;

    float spawnTimer = 0.0f;
    float nextRandomSpawnTime;

    private void Update()
    {
        //SpawnNextEnemy();
    }

    void SpawnEnemyOnSpline(int enemyDamageVal)
    { 
        if(!enemyPrefab || !splineContainer)
            return;

        float randomPos = Random.Range(0f, 1f);

        Vector3 spawnPosition =  splineContainer.EvaluatePosition(randomPos);

        BaseEnemy newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity).GetComponent<BaseEnemy>();
        newEnemy.IncreaseEnemyDamage(enemyDamageVal);

        nextRandomSpawnTime = Random.Range(MinNextSpawnTime, MaxNextSpawnTime);
    }

    public void SpawnNextEnemy(int enemyDamageVal)
    {
        if (spawnTimer < nextRandomSpawnTime)
        {
            spawnTimer += Time.deltaTime;
        }

        else
        { 
            SpawnEnemyOnSpline(enemyDamageVal);
            spawnTimer = 0.0f;
        }


    }

}

using UnityEngine;

[CreateAssetMenu(fileName = "SpawnableItem", menuName = "Scriptable Objects/SpawnableItem")]
public class SpawnableItem : ScriptableObject
{
    [Header("Prefab Reference")]

    [SerializeField] GameObject itemPrefab;

    [Space(5)]

    [Header("Spawn Attributes")]

    [SerializeField] int minNumToSpawn = 1;
    [SerializeField] int maxNumToSpawn = 5;

    [Space(2)]

    [SerializeField] float minSpawnOffset = -3f;
    [SerializeField] float maxSpawnOffset = 3f;

    [Space(2)]

    [Tooltip("Percentage of chance to spawn item when owner is looted / destroyed (Between 0.01% and 100% in float)")]
    [SerializeField, Range(0f, 100f)] float chanceToSpawn = 75.0f;

    [Space(2)]

    [SerializeField] bool isOnlySpawnedOnce = false;

    // Public properties

    public float ChanceToSpawn => chanceToSpawn;
    public float MinSpawnOffset => minSpawnOffset;
    public float MaxSpawnOffset => maxSpawnOffset;
    public int MinNumToSpawn => minNumToSpawn;
    public int MaxNumToSpawn => maxNumToSpawn;

    public GameObject ItemPrefab => itemPrefab;

    public bool IsOnlySpawnedOnce => isOnlySpawnedOnce;

}

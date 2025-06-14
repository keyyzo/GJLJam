using UnityEngine;

public class SimpleRangeAttack : BaseAttack
{
    [Header("References")]

    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform projectileSpawnPoint;

    [Space(5)]

    [Header("Range Attributes")]

    [SerializeField] int maxAmmo = 999;
    [SerializeField] int currentAmmo = 30;
    [SerializeField] float rangeRateOfFire = 0.5f;
    [SerializeField] float projectileSpeed = 15.0f;

    // Private variables

    float _rangeAttackTimer = 0.0f;

    bool _canAttack = true;

    public override void Attack()
    {
        if (_canAttack)
        { 
            
        }
    }

    void FireProjectile()
    { 
        
    }
}

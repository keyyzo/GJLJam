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

    Transform playerAimPoint;

    float _rangeAttackTimer = 0.0f;

    bool _canAttack = true;

    private void Start()
    {
        playerAimPoint = GameObject.Find("AimPointer").transform;
    }

    private void Update()
    {
        RangeAttackReset();
    }

    public override void Attack()
    {
        if (_canAttack)
        {
            FireProjectile();
            _canAttack = false;
            currentAmmo -= 1;
        }
    }

    public void AdjustAmmo(int amount)
    {
        currentAmmo += amount;

        if (currentAmmo > maxAmmo)
        { 
            currentAmmo = maxAmmo;
        }
    }

    void FireProjectile()
    {
        Vector3 newAimPoint = new Vector3(playerAimPoint.position.x, transform.position.y, playerAimPoint.position.z);
        Vector3 tempProjectileDirection = newAimPoint - transform.position;

        //transform.LookAt(newAimPoint);

        Projectile newProjectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity).GetComponent<Projectile>();
        newProjectile?.ProcessProjectile(projectileSpeed, tempProjectileDirection, attackDamage);
    }

    void RangeAttackReset()
    {
        if (!_canAttack && currentAmmo > 0)
        {
            if (_rangeAttackTimer < rangeRateOfFire)
            {
                _rangeAttackTimer += Time.deltaTime;
            }

            else
            {
                _rangeAttackTimer = 0.0f;
                _canAttack = true;
            }
        }
    }
}

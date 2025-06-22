using UnityEngine;

public class SimpleRangeAttack : BaseAttack
{
    [Header("References")]

    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform projectileSpawnPoint;
    

    [Space(5)]

    [Header("Range Attributes")]

    [SerializeField] int maxClipSize = 15;
    [SerializeField] int currentAmmoInClip = 15;
    [SerializeField] float rangeRateOfFire = 0.5f;
    [SerializeField] float projectileSpeed = 15.0f;

    [Space(5)]

    [Header("Reloading Attributes")]

    [SerializeField] float timeToReload = 2.5f;

    // Private variables

    Transform playerAimPoint;

    float _rangeAttackTimer = 0.0f;
    float _reloadingTimer = 0.0f;

    bool _canAttack = true;
    bool _isReloading = false;

    // Cached Components

    PlayerInventory playerInventory;

    private void Start()
    {
        playerAimPoint = GameObject.Find("AimPointer").transform;
        playerInventory = GetComponentInParent<PlayerInventory>();
    }

    private void Update()
    {
        RangeAttackReset();
        ReloadTimer();
        UIManager.Instance.SetPlayerClipAmmo(currentAmmoInClip);
    }

    public override void Attack()
    {
        if (_canAttack && !_isReloading && currentAmmoInClip > 0)
        {
            FireProjectile();
            _canAttack = false;
            currentAmmoInClip -= 1;
            Debug.Log("Current ammo in clip: " + currentAmmoInClip);
        }
    }

    public void ReloadAmmo(int amount)
    {
        currentAmmoInClip += amount;

        if (currentAmmoInClip > maxClipSize)
        { 
            currentAmmoInClip = maxClipSize;
        }
    }

    public void ProcessReload()
    {
        if (!_isReloading && currentAmmoInClip < maxClipSize && playerInventory.CurrentAmmoAmount > 0)
        {
            Debug.Log("Reloading...");
            _isReloading = true;
            //playerInventory.ReloadAttackAmmo(currentAmmoInClip, maxClipSize);
            currentAmmoInClip += playerInventory.ReloadAttackAmmo(currentAmmoInClip, maxClipSize);
        }

        else
        {
            Debug.Log("Can't reload yet");
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
        if (!_canAttack && currentAmmoInClip > 0)
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

    void ReloadTimer()
    {
        if (_isReloading)
        {
            if (_reloadingTimer < timeToReload)
            {
                _reloadingTimer += Time.deltaTime;
            }

            else
            { 
                _reloadingTimer = 0.0f;
                _isReloading = false;
            }
        }
    }


}

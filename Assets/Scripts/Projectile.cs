using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float maxProjectileLife = 8.0f;

    // Private variables

    protected int damageToDeal;

    protected float projectileLifetimer = 0.0f;

    // Cached components

    protected Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        HandleProjectileLife();
    }


    public void ProcessProjectile(float projectileSpeed, Vector3 projectileDirection, int damage)
    {
        damageToDeal = damage;

        //Vector3 finalVelocity = new Vector3(projectileDirection.x * projectileSpeed, projectileDirection.y * projectileSpeed, projectileDirection.z * projectileSpeed);

        Vector3 finalVelocity = projectileDirection.normalized * projectileSpeed;

        //rb.AddForce(projectileDirection * (projectileSpeed * Time.deltaTime), ForceMode.Impulse);

        rb.AddForce(finalVelocity * Time.deltaTime, ForceMode.Impulse);

        //rb.linearVelocity = (projectileDirection * Time.deltaTime) * projectileSpeed;
        //rb.linearVelocity = finalVelocity * Time.deltaTime;
    }

    void HandleProjectileLife()
    { 
        //projectileLifetimer += Time.deltaTime;

        //if (projectileLifetimer >= maxProjectileLife)
        //{
        //    Destroy(gameObject);
        //}

        Destroy(gameObject, maxProjectileLife);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable<int> damageableEntity))
        {
            damageableEntity.ProcessDamage(damageToDeal);

            if (damageableEntity.CurrentHealth <= 0 && other.TryGetComponent(out BaseEnemy enemyHit))
            {
                enemyHit.ProcessSpawn();
            }
        }

        //if (other.TryGetComponent(out BaseEnemy enemyHit))
        //{
        //    enemyHit.ProcessSpawn();
        //}


        Destroy(gameObject);
    }

}

using Unity.VisualScripting;
using UnityEditor.Compilation;
using UnityEngine;

public class SimpleMeleeAttack : BaseAttack
{
    [Header("References")]

    //[SerializeField] GameObject meleePrefab;
    [SerializeField] Animator meleeAnimator;

    [Space(5)]

    [Header("Melee Attributes")]

    [SerializeField] float meleeAttackRate = 0.5f;

    const string ATTACK_STRING = "AttackTrigger";

    // private variables

    float _meleeAttackTimer = 0.0f;

    bool _canAttack = true;

    // Cahced components

    CapsuleCollider _meleeCollider;


    private void Awake()
    {
        _meleeCollider = GetComponent<CapsuleCollider>();
    }

    private void Start()
    {
        meleeAnimator.Play("Melee_Idle");
    }

    private void Update()
    {
        MeleeAttackReset();
        //Debug.Log("_canAttack = " + _canAttack);
    }


    public override void Attack()
    {
        if (_canAttack) 
        {
            meleeAnimator.StopPlayback();
            //meleeAnimator.SetBool(ATTACK_STRING, _canAttack);
            meleeAnimator.SetTrigger(ATTACK_STRING);
            _canAttack = false;

            Debug.Log("Attacking with melee");
        }

        
    }

    void MeleeAttackReset()
    {
        if (!_canAttack && meleeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Melee_Idle"))  
        {
            //meleeAnimator.SetBool(ATTACK_STRING, _canAttack);

            if (_meleeAttackTimer < meleeAttackRate)
            {
                _meleeAttackTimer += Time.deltaTime;
            }

            else
            {
                _meleeAttackTimer = 0.0f;
                _canAttack = true;
                meleeAnimator.ResetTrigger(ATTACK_STRING);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out IDamageable<int> enemyToDamage))
        {
            Debug.Log("Attempting to damage something");
            enemyToDamage?.ProcessDamage(attackDamage);
        }

        if (collision.collider.TryGetComponent(out BaseHealthComponent enemyHealth))
        {
            Debug.Log("Attempting to damage something");
            enemyHealth?.ProcessDamage(attackDamage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable<int> enemyToDamage))
        {
            Debug.Log("Attempting to damage something");
            enemyToDamage?.ProcessDamage(attackDamage);
        }

        if (other.TryGetComponent(out BaseHealthComponent enemyHealth))
        {
            Debug.Log("Attempting to damage something");
            enemyHealth?.ProcessDamage(attackDamage);
        }

        Debug.Log(other.gameObject.name);
    }

}

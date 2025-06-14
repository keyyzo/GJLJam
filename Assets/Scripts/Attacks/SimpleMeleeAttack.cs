using Unity.VisualScripting;
using UnityEditor.Compilation;
using UnityEngine;

public class SimpleMeleeAttack : BaseAttack
{
    [Header("References")]

    [SerializeField] Animator meleeAnimator;

    [Space(5)]

    [Header("Melee Attributes")]

    [SerializeField] float meleeAttackRate = 0.5f;
    [SerializeField] float meleeRangeOffset = 0.0f;

    const string ATTACK_STRING = "AttackTrigger";

    // private variables

    float _meleeAttackTimer = 0.0f;

    bool _canAttack = true;

    Vector3 tempOldPos;
    Vector3 tempOldScale;

    // Cahced components

    CapsuleCollider _meleeCollider;


    private void Awake()
    {
        _meleeCollider = GetComponent<CapsuleCollider>();
    }

    private void Start()
    {
        meleeAnimator.Play("Melee_Idle");
        tempOldPos = transform.localPosition;
        tempOldScale = transform.localScale;
    }

    private void Update()
    {
       //ProcessMeleeRange();
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

            Vector3 newPos = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + (meleeRangeOffset * 2f ) );

            Vector3 newScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z + meleeRangeOffset);

            transform.localPosition = newPos;
            transform.localScale = newScale;

            Debug.Log("Attacking with melee");
        }

        
    }

    void ProcessMeleeRange()
    {
        Vector3 newPos = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + meleeRangeOffset);

        Vector3 newScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z + meleeRangeOffset);


        if (meleeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Melee_Attack"))
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + meleeRangeOffset);
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z + meleeRangeOffset);

            transform.localPosition = newPos;
            transform.localScale = newScale;
        }

        else
        {
            transform.localPosition = tempOldPos;
            transform.localScale = tempOldScale;

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

                transform.localPosition = Vector3.Lerp(transform.localPosition, tempOldPos, Time.deltaTime * 6f);
                transform.localScale = Vector3.Lerp(transform.localScale, tempOldScale, Time.deltaTime * 6f);

            }

            else
            {
                transform.localPosition = tempOldPos;
                transform.localScale = tempOldScale;

                _meleeAttackTimer = 0.0f;
                _canAttack = true;
                meleeAnimator.StopPlayback();
                meleeAnimator.SetTrigger("IdleTrigger");
                meleeAnimator.ResetTrigger(ATTACK_STRING);
            }
        }
    }

  

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable<int> enemyToDamage))
        {
            Debug.Log("Attempting to damage something");
            enemyToDamage?.ProcessDamage(attackDamage);
        }

        Debug.Log(other.gameObject.name);
    }

}

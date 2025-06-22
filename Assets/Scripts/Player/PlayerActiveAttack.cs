using UnityEngine;

public class PlayerActiveAttack : MonoBehaviour
{
    [SerializeField] BaseAttack starterAttack;



    // Cached Components

    BaseAttack currentAttack;

    private void Awake()
    {
        if(starterAttack)
            SwitchAttack(starterAttack);
    }

    private void Update()
    {
        if (currentAttack != null && currentAttack is SimpleRangeAttack)
        {
            UIManager.Instance.isRangeWeaponActive = true;
        }

        else
        {
            UIManager.Instance.isRangeWeaponActive = false;
        }

    }

    public void HandleAttack(bool attackInput)
    { 
        if (attackInput) 
        {
            currentAttack?.Attack();
        }
    }

    public void SwitchAttack(BaseAttack newAttack)
    {
        if (currentAttack)
        { 
           Destroy(currentAttack.gameObject);
        }

        BaseAttack newInstAttack = Instantiate(newAttack, transform).GetComponent<BaseAttack>();
        currentAttack = newInstAttack;
    }

    public void ReloadAttack(bool reloadInput)
    {
        if (reloadInput && currentAttack is SimpleRangeAttack)
        {
            currentAttack?.GetComponent<SimpleRangeAttack>().ProcessReload();
        }
    }
}

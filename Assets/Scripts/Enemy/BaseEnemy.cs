using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    


    // Cached components

    BaseHealthComponent healthComponent;


    private void Awake()
    {
        healthComponent = GetComponent<BaseHealthComponent>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

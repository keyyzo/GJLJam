using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Following Unity documentation for learning implementation,
/// this singleton will act as the base for any possible singleton
/// that is needed throughout the project that also needs to be persistent
/// </summary>
public class GenericPersistentSingleton<T> : MonoBehaviour where T : Component
{
    private static T m_Instance;

    public static T Instance
    {
        get
        {
            // Checks if singleton currently exists or not, if so finds it within the scene

            if (m_Instance == null)
            { 
                m_Instance = FindFirstObjectByType<T>();

                // Create a T Type object if it doesn't exist

                if (m_Instance == null)
                {
                    GameObject singletonObject = new GameObject();

                    m_Instance = singletonObject.AddComponent<T>();

                    // Name the singleton instance to the Type

                    singletonObject.name = typeof(T).ToString();

                    DontDestroyOnLoad(singletonObject);
                }
            }

            return m_Instance;
        }
    }

    protected virtual void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this as T;

            DontDestroyOnLoad(this.gameObject);
        }

        else if (m_Instance != this)
        {
            Destroy(gameObject);
        }
    }
}

using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    bool isInteracting = false;

    IInteractable currentInteractable;

    public void SetInteracting(bool interactInput)
    { 
        isInteracting = interactInput;
    }

    private void Update()
    {
        HandleInteraction();
    }

    void HandleInteraction()
    {
        if (currentInteractable != null)
        {
            if (isInteracting)
            {
                currentInteractable.ProcessInteract();
            }
        }
    }

    void SetCurrentInteractable(IInteractable newInteractable)
    {
        if (newInteractable != null)
        {
            currentInteractable = newInteractable;
            currentInteractable.ProcessInteractPrompt();
            Debug.Log("Interactable Set");
        }

    }

    void DisableCurrentInteractable()
    {
        if (currentInteractable != null)
        { 
            currentInteractable.CancelInteractPrompt();
            currentInteractable = null;
            Debug.Log("Interactable Disabled");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactableObject))
        {
            SetCurrentInteractable(interactableObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactableObject))
        {
            DisableCurrentInteractable();
        }
    }
}

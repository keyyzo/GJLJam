using UnityEngine;

public interface IInteractable
{
    void ProcessInteract();
    void ProcessInteractPrompt();
    void CancelInteractPrompt();

}

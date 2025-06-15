using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    [Header("Character Input Values")]

    [SerializeField] Vector2 move;
    public Vector2 Move => move;

    [SerializeField] Vector2 aim;
    public Vector2 Aim => aim;

    [SerializeField] bool attack;
    public bool Attack => attack;

    [SerializeField] bool interact;
    public bool Interact => interact;

    [SerializeField] bool reload;
    public bool Reload => reload;   

    [Space(10)]

    [Header("Mouse Cursor Settings")]

    public bool CursorLocked = true;

    private void Start()
    {
        SetCursorState(CursorLocked);
        SetCursorHidden(false);
    }

    #region Callback Functions

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        aim = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        //if (context.performed)
        //{
        //    attack = true;
        //}

        //if(context.canceled)
        //{
        //    attack = false;
        //}

        attack = context.ReadValueAsButton();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        interact = context.ReadValueAsButton();
    }

    public void OnReload(InputAction.CallbackContext context)
    { 
        reload = context.ReadValueAsButton();
    }

    #endregion


    public void SetCursorState(bool newState)
    {
        CursorLocked = newState;
        Cursor.lockState = newState ? CursorLockMode.Confined : CursorLockMode.None;
    }

    public void SetCursorHidden(bool newState)
    { 
        Cursor.visible = newState;
    }
}

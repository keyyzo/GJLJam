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

    [SerializeField] bool pause;
    public bool Pause => pause;

    [SerializeField] float weaponSlots;
    public float WeaponSlots => weaponSlots;

    [SerializeField] float scroll;
    public float Scroll => scroll;

    [Space(10)]

    [Header("Mouse Cursor Settings")]

    public bool CursorLocked = true;

    public bool CursorVisible = true;

    private void Start()
    {
        SetCursorState(CursorLocked);
        SetCursorHidden(CursorVisible);
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
        if (context.performed)
        {
            attack = true;
        }

        if (context.canceled)
        {
            attack = false;
        }

        //attack = context.ReadValueAsButton();

        //attack = context.action.triggered;

        //attack = context.action.WasPressedThisFrame();

    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        interact = context.ReadValueAsButton();
    }

    public void OnReload(InputAction.CallbackContext context)
    { 
        reload = context.ReadValueAsButton();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
       pause = context.ReadValueAsButton();
    }

    public void OnWeaponSlots(InputAction.CallbackContext context)
    {
        
        if (context.performed)
        {
            weaponSlots = context.ReadValue<float>();
        }

        Debug.Log("Testing slot value button: " + weaponSlots);
    }

    

    public void OnWeaponScroll(InputAction.CallbackContext context)
    {
        scroll = context.ReadValue<float>();
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

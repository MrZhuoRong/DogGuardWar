using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class GameInput : MonoBehaviour,CourseGameInputAction.IPlayerActions
{
    public CourseGameInputAction inputActions;

    //public Action jumpAction;
    public Action<bool> AttackAction;
    public Action<Vector2> moveAction;
    public Action<Vector2> lookAction;
    public Action aimAction;

    // Start is called before the first frame update
    private void Awake()
    {
        inputActions = new CourseGameInputAction();
    }
    
    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.SetCallbacks(this);        
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    public void OnMove(CallbackContext context)
    { 
        moveAction?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnLook(CallbackContext context)
    {
        lookAction?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnAttack(CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
            AttackAction?.Invoke(true);
        if (context.phase == InputActionPhase.Canceled)
            AttackAction?.Invoke(false);
    }

    public void OnJump(CallbackContext context)
    {
        //if (context.phase == InputActionPhase.Started)
        //    jumpAction?.Invoke();
        //if (context.phase == InputActionPhase.Canceled)
        //jumpAction?.Invoke(false);
    }

    public void OnAim(CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
            aimAction?.Invoke();
    }
}




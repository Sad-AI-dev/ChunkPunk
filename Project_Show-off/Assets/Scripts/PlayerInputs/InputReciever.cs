using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReciever : MonoBehaviour
{
    [SerializeField] bool linkOnStart = true;
    [HideInInspector] public int id = 0;
    Player target;

    private void Start()
    {
        if (linkOnStart) Link();
    }

    public void Link()
    {
        target = PlayerManager.instance.GetUnlinkedPlayer();
        if (!target) { Destroy(gameObject); } //make sure an unlinked player exists
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (target) { target.SetMoveDir(context.ReadValue<Vector2>()); }
    }

    public void Looking(InputAction.CallbackContext context)
    {
        if (target) {
            if (context.canceled) { target.Look(Vector2.zero); }
            else { target.Look(context.ReadValue<Vector2>()); }
        }
    }
    
    public void SouthButton(InputAction.CallbackContext context)
    {
        /*
        if (context.started) {
            if (target) { target.Shoot(); }
        }
        */
        //Disable for test
        /*
        if (target)
        {
            if (context.started)
            {
                StartCoroutine(target.Shoot(true));
            }
            else if (context.canceled)
            {
                StartCoroutine(target.Shoot(false));

            }
        }
        */
    }

    public void Aim(InputAction.CallbackContext context)
    {
        if (target) {
            if (context.started) {
                target.Aim(true);
            }
            else if (context.canceled) {
                target.Aim(false);
            }
        }
    }

    public void Accelerate(InputAction.CallbackContext context)
    {
        if (target)
        {
            if (context.started)
            {
                Debug.Log("Acelerating");
                target.Accelerate(true);
            }
            else if (context.canceled)
            {
                target.Accelerate(false);
            }
        }
    }
    public void Deccelerate(InputAction.CallbackContext context)
    {
        if (target)
        {
            if (context.started)
            {
                Debug.Log("Decelerating");
                target.Decelerate(true);
            }
            else if (context.canceled)
            {
                target.Decelerate(false);
            }
        }
    }

    public void RightBumper(InputAction.CallbackContext context)
    {
        if (target) {
            if (context.started) {
                target.inventory.SelectItem();
            }
            else if (context.canceled) {
                target.inventory.UseItem();
            }
        }
    }
}

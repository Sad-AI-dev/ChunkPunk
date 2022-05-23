using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReciever : MonoBehaviour
{
    Player target;

    private void Start()
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
        if (context.started) {
            if (target) { target.Shoot(); }
        }
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
}

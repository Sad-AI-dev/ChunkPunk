using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReciever : MonoBehaviour
{
    Player target;
    Respawn respawn;

    private void Start()
    {
        target = PlayerManager.instance.GetUnlinkedPlayer();
        if (!target) { Destroy(gameObject); } //make sure an unlinked player exists
    }

    public void Move(InputAction.CallbackContext context)
    {
        target?.SetMoveDir(context.ReadValue<Vector2>());
    }

    public void Accelerate(InputAction.CallbackContext context)
    {
        Vector3 playerSpeed = context.ReadValue<Vector2>();
        if (playerSpeed.y > 0.2)
        {
            target.Accelerating(true);

        }
        else
        {
            target.Accelerating(false);
        }
    }

    public void Slow(InputAction.CallbackContext context)
    {
        Vector3 playerSpeed = context.ReadValue<Vector2>();
        if(playerSpeed.y < -0.2)
        {
            target.Slowing(true);

        } else
        {
            target.Slowing(false);
        }
    }


    public void Die(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            target?.Died();
        }
    }
    public void Looking(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        Debug.Log("at all");
        if(direction.x < -0.2)
        {
            target?.Look(-1);
            Debug.Log("Here");
        } else if(direction.x > 0.2)
        {
            target?.Look(1);
        }

        if (context.canceled)
        {
            target?.Look(0);
        }
    }
    
    public void SouthButton(InputAction.CallbackContext context)
    {
        if (context.started) {
            target?.Shoot();
        }
    }

    public void Aim(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            target?.Aim(true);
        } else if (context.canceled)
        {
            target?.Aim(false);
        }
    }
}

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
        target?.SetMoveDir(context.ReadValue<Vector2>());
    }

    public void SouthButton(InputAction.CallbackContext context)
    {
        if (context.started) {
            target?.Shoot();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputTest : MonoBehaviour
{
    Vector2 toMove;
    [SerializeField] float moveSpeed;

    public void Move(InputAction.CallbackContext context)
    {
        toMove = context.ReadValue<Vector2>();
    }

    public void SouthButton(InputAction.CallbackContext context)
    {
        if (context.started) {
            Debug.Log("Pressed button");
        }
        else if (context.canceled) {
            Debug.Log("Released button!");
        }
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(toMove.x, 0, toMove.y) * (moveSpeed * Time.deltaTime);
    }
}
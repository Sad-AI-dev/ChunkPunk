using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody human;
    private PlayerInput playerInput;

    private void Awake()
    {
        human = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        playerInput.onActionTriggered += playerInput_onAction;
    }


    private void playerInput_onAction(InputAction.CallbackContext context)
    {
        Debug.Log(context);
    }

    public void Movement(InputAction.CallbackContext context)
    {
        human.AddForce(0, 0, 2);
    }
}

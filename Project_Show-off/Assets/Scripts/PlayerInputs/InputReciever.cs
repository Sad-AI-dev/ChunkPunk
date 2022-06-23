using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputReciever : MonoBehaviour
{
    [SerializeField] bool linkOnStart = true;
    [HideInInspector] public int id = 0;
    Player target;

    //--------------------------------linking-------------------------------------
    private void Start()
    {
        if (linkOnStart) StartCoroutine(LinkCo());
        else { SceneManager.sceneLoaded += OnSceneLoaded; }
    }

    IEnumerator LinkCo()
    {
        target = null;
        yield return null; //wait a frame
        if (PlayerManager.instance) {
            target = PlayerManager.instance.GetUnlinkedPlayer();
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(LinkCo());
    }
    
    //--------------------unlink---------------
    public void Unlink()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Destroy(gameObject);
    }

    //---------------------------inputs--------------------------------------------
    public void Move(InputAction.CallbackContext context)
    {
        if (target) { target.SetMoveDir(context.ReadValue<Vector2>()); }
    }

    public void UTurn(InputAction.CallbackContext context)
    {
        if (target) { target.UTurn(); }
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
        
        if (target)
        {
            if (context.started)
            {
                target.isShoting = true;
                //StartCoroutine(target.isShooting());
            }
            
            else if (context.canceled)
            {
                target.isShoting = false;

            }
            
        }
        
    }


    public void UseMap(InputAction.CallbackContext context)
    {
        if (target) {
            if (context.started) { target.ShowMap(); }
            else if (context.canceled) { target.HideMap(); }
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
    
    public void Accelerate(InputAction.CallbackContext context)
    {
        if (target)
        {
            if (context.started) {
               target.Accelerate(true);
            }
            else if (context.canceled) {
                target.Accelerate(false);
            }
        }
    }
    
    public void Deccelerate(InputAction.CallbackContext context)
    {
        if (target)
        {
            if (context.started) {
                target.Decelerate(true);
            }
            else if (context.canceled) {
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

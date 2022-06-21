using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    public UnityEvent stun;

    private void Awake()
    {
        if (stun == null)
            stun = new UnityEvent();

        stun.AddListener(stunned);
    }


    private void stunned()
    {
        animator.SetTrigger("stunned");
        Debug.Log("Stunne");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class animationStateController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [HideInInspector] public UnityEvent stun;
    [HideInInspector] public UnityEvent Idle;
    [HideInInspector] public UnityEvent Skate;


    private void Awake()
    {
        if (stun == null)
            stun = new UnityEvent();

        if (Idle == null)
            Idle = new UnityEvent();

        if (Skate == null)
            Skate = new UnityEvent();

        stun.AddListener(stunned);
        Idle.AddListener(Idling);
        Skate.AddListener(Skating);
    }


    private void stunned()
    {
        animator.SetTrigger("stunned");
    }

    private void Idling()
    {
        animator.SetTrigger("Idle");
    }

    private void Skating()
    {
        animator.SetTrigger("Skating");
    }
}

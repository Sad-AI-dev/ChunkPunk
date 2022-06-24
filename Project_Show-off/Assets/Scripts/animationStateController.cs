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

    enum animState
    {
        idle,
        skate,
        stun
    }
    animState state;

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
        //start in idle
        state = animState.idle;
        animator.SetTrigger("Idle");
    }


    private void stunned()
    {
        if (state != animState.stun) {
            animator.SetTrigger("stunned");
            state = animState.stun;
        }
    }

    private void Idling()
    {
        if (state != animState.idle) {
            animator.SetTrigger("Idle");
            state = animState.idle;
        }
    }

    private void Skating()
    {
        if (state != animState.skate) {
            animator.SetTrigger("Skating");
            state = animState.skate;
        }
    }
}

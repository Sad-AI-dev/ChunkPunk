using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Goal : MonoBehaviour
{
    public UnityEvent onReachGoal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            onReachGoal?.Invoke();
            Destroy(gameObject);
        }
    }
}

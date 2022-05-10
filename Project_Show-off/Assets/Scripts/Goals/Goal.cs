using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Goal : MonoBehaviour
{
    public UnityEvent<Player> onReachGoal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            onReachGoal?.Invoke(other.GetComponent<Player>());
            Destroy(gameObject);
        }
    }
}
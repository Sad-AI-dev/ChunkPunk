using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    Player target;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("DeathDoBe");
            Player tempPlayer = other.GetComponent<Player>();
            tempPlayer?.Died();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int value = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            CoinManager.instance.GainMoney(other.GetComponent<Player>(), value);
            Destroy(gameObject);
        }
    }
}
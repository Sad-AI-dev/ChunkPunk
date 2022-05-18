using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] int value = 1;
    [SerializeField] float minRespawnTime = 5f;
    [SerializeField] float maxRespawnTime = 15f;

    [Header("Technical settings")]
    [SerializeField] GameObject visuals;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && visuals.activeSelf) {
            CoinManager.instance.GainMoney(other.GetComponent<Player>(), value);
            StartCoroutine(RespawnCo());
        }
    }

    IEnumerator RespawnCo()
    {
        visuals.SetActive(false);
        yield return new WaitForSeconds(Random.Range(minRespawnTime, maxRespawnTime));
        visuals.SetActive(true);
    }
}
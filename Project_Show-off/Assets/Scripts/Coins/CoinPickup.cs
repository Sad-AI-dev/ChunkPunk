using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] int value = 1;
    [SerializeField] float minRespawnTime = 5f;
    [SerializeField] float maxRespawnTime = 15f;

    [Header("Technical settings")]
    [SerializeField] GameObject visuals;
    [SerializeField] UnityEvent onPickup;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && visuals.activeSelf && CoinManager.instance.money[other.GetComponent<Player>()] < CoinManager.instance.maximumBullets) {
            CoinManager.instance.GainMoney(other.GetComponent<Player>(), value);
            onPickup?.Invoke();
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
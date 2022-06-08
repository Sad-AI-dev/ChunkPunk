using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] List<WeightedLink<InventoryItem>> itemChances;

    [SerializeField] UnityEvent onPickup;

    [Header("Technical Settings")]
    [SerializeField] float minDisableTime = 5f;
    [SerializeField] float maxDisableTime = 10f;
    [SerializeField] GameObject visuals;

    private void OnTriggerEnter(Collider other)
    {
        if (visuals.activeSelf && other.CompareTag("Player")) {
            if (other.GetComponent<Player>().inventory.TryGetItem(GetRandomItem())) {
                //item picked up, disable
                onPickup?.Invoke();
                StartCoroutine(DisableCo());
            }
        }
    }

    IEnumerator DisableCo()
    {
        visuals.SetActive(false);
        yield return new WaitForSeconds(Random.Range(minDisableTime, maxDisableTime));
        visuals.SetActive(true);
    }

    InventoryItem GetRandomItem()
    {
        float r = Random.Range(0f, GetTotalWeight());
        int index = 0;
        while (r > 0) {
            if (r < itemChances[index].chance) { return itemChances[index].obj; }
            else {
                r -= itemChances[index].chance;
                index++;
            }
        }
        return null;
    }

    float GetTotalWeight()
    {
        float output = 0f;
        foreach (WeightedLink<InventoryItem> chance in itemChances) {
            output += chance.chance;
        }
        return output;
    }
}

[System.Serializable]
public class WeightedLink<T>
{
    public T obj;
    public float chance = 1f;
}

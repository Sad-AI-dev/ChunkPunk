using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] float delay;

    void Start()
    {
        StartCoroutine(SpawnCo());
    }

    IEnumerator SpawnCo()
    {
        yield return new WaitForSeconds(delay);
        Instantiate(prefab, transform);
    }
}

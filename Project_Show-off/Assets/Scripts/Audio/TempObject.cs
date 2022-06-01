using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempObject : MonoBehaviour
{
    [SerializeField] float lifeTime;

    private void Awake()
    {
        StartCoroutine(LifeTimeCo());
    }

    IEnumerator LifeTimeCo()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}

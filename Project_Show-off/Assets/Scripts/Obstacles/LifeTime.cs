using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    [SerializeField] private float lifeTime;

    private void Start()
    {
        StartCoroutine(lifeTimeCo());
    }

    private IEnumerator lifeTimeCo()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}

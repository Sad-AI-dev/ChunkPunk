using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float lifeTime = 5f;

    [HideInInspector] public Player owner;

    private void Update()
    {
        StartCoroutine(LifeTimeCo());
    }
    //-----------------lifetime----------------
    IEnumerator LifeTimeCo()
    {
        yield return new WaitForSeconds(lifeTime);
        StartCoroutine(DieCo());
    }

    //---------------movement----------------
    private void FixedUpdate()
    {
        transform.Translate(0, 0, moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != owner.gameObject) {
            StartCoroutine(DieCo());
        }
    }
    IEnumerator DieCo()
    {
        yield return null;
        Destroy(gameObject);
    }
}
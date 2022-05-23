using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float lifeTime = 5f;
    [SerializeField] UnityEvent onHit = new();

    [HideInInspector] public Player owner;

    void Start()
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
        transform.position += transform.forward * (moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform != owner.transform) {
            onHit?.Invoke();
            StartCoroutine(DieCo());
        }
    }
    IEnumerator DieCo()
    {
        yield return null;
        Destroy(gameObject);
    }
}
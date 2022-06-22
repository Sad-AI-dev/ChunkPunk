using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    private readonly float setupTime = 0.1f;
    private bool starting = true;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float lifeTime = 5f;
    [SerializeField] UnityEvent onHit = new();

    void Start()
    {
        starting = true;
        StartCoroutine(LifeTimeCo());
    }
    //-----------------lifetime----------------
    IEnumerator LifeTimeCo()
    {
        yield return new WaitForSeconds(setupTime);
        starting = false;
        yield return new WaitForSeconds(lifeTime - setupTime);
        StartCoroutine(DieCo());
    }

    //---------------movement----------------
    private void FixedUpdate()
    {
        transform.localPosition += transform.forward * (moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!starting) {
            onHit?.Invoke();
            StartCoroutine(DieCo());
            //destroy obstacle?
            if (collision.transform.CompareTag("Obstacle")) {
                if (!collision.transform.TryGetComponent(out LandMine landMine)) {
                    DestroyObject(collision.gameObject);
                }
            }
        }
    }
    IEnumerator DieCo()
    {
        yield return null;
        Destroy(gameObject);
    }

    private void DestroyObject(GameObject target)
    {
        Destroy(target);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GetHit : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float X_ZAxisForce;
    [SerializeField] int bulletForce;
    [SerializeField] private int stunTime;
    [SerializeField] private float yKnockback;
    [SerializeField] private float invicibilityTime;
    private bool alreadyHit;
    Player thisPlayer;

    //events
    [SerializeField] UnityEvent onGetHit;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        thisPlayer = GetComponent<Player>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Projectile") && !alreadyHit)
        {
            GameObject projectile = collision.gameObject;
            Vector3 direction = projectile.transform.forward * X_ZAxisForce;
            direction.y = yKnockback;
            playerHit(direction.normalized);
            alreadyHit = true;
            StartCoroutine(stunned());
            StartCoroutine(cantHit());
            Debug.Log("hit");
            onGetHit?.Invoke();
        }
    }

    private void playerHit(Vector3 bulletDir)
    {
        Debug.Log("apply ze force");
        Debug.Log(bulletDir);
        //rb.AddForce(bulletDir * 100000);
        //rb.AddExplosionForce(bulletForce * 100, bulletDir, 4);
        //rb.AddRelativeForce(bulletDir * 40);
        rb.AddForce(bulletDir * bulletForce);
    }

    private IEnumerator stunned()
    {
        thisPlayer.isStunned = true;
        yield return new WaitForSeconds(stunTime);
        thisPlayer.isStunned = false;
    }

    private IEnumerator cantHit()
    {
        yield return new WaitForSeconds(invicibilityTime);
        alreadyHit = false;
    }
}

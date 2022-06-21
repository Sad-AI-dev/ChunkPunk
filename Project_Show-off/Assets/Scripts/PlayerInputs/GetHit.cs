using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GetHit : MonoBehaviour
{
    [SerializeField] float X_ZAxisForce;
    [SerializeField] int bulletForce;
    [SerializeField] private int stunTime;
    [SerializeField] private float yKnockback;
    [SerializeField] private float invicibilityTime;
    //state
    [HideInInspector] public bool alreadyHit;

    //external components
    Rigidbody rb;
    Player thisPlayer;

    //events


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
            HitPlayer(direction.normalized);
            //stun the player
            StunPlayer(stunTime, invicibilityTime);
        }
    }

    private void HitPlayer(Vector3 bulletDir)
    {
        rb.AddForce(bulletDir * bulletForce);
    }

    public void StunPlayer(float stunDuration, float invinceDuration)
    {
        alreadyHit = true;
        StartCoroutine(StunCo(stunDuration));
        StartCoroutine(InvinceCo(invinceDuration));
    }

    private IEnumerator StunCo(float duration)
    {
        thisPlayer.isStunned = true;
        thisPlayer.stunned();
        yield return new WaitForSeconds(duration);
        thisPlayer.isStunned = false;
    }

    private void stunned()
    {

    }

    private IEnumerator InvinceCo(float duration)
    {
        yield return new WaitForSeconds(duration);
        alreadyHit = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class LandMine : MonoBehaviour
{
    [SerializeField] Vector3 force;
    [SerializeField] float radius;

    [Header("Player Hit Settings")]
    [SerializeField] float stunDuration = 1f;
    [SerializeField] float invinceDuration = 1.2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {
            Explode();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile")) {
            Explode();
        }
    }

    void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hitCollider in hitColliders) {
            EffectExternalObjects(hitCollider.gameObject);
        }
        Destroy(gameObject);
    }

    void EffectExternalObjects(GameObject obj)
    {
        if (obj.CompareTag("Player")) {
            if (obj.TryGetComponent(out Player player)) {
                //apply knockback + stun
                player.rb.velocity = Vector3.zero;
                player.rb.AddRelativeForce(force * 10, ForceMode.Impulse);
                //stun player
                player.getHit.StunPlayer(stunDuration, invinceDuration);
            }
        } 
        else if (obj.CompareTag("Obstacle")) {
            //destroy obstacles
            Destroy(obj);
        }
    }
}

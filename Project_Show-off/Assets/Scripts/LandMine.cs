using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : MonoBehaviour
{
    [SerializeField] Vector3 force;
    [SerializeField] float radius;
    
    /*
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player");
            other.transform.TryGetComponent(out Player player);
            Rigidbody body = player.GetComponent<Rigidbody>();
            body.AddForceAtPosition(force, this.transform.position);
        }
    }
    */
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            //collision.transform.TryGetComponent(out Projectile projectile);
            Debug.Log("hit with bullet");
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Player"))
                {
                    Rigidbody body = hitCollider.GetComponent<Rigidbody>();
                    body.AddForceAtPosition(force, this.transform.position);
                } else if (hitCollider.CompareTag("Obstacle"))
                {
                    Debug.Log("Yoooooooooooooooooooo");
                    Destroy(hitCollider.gameObject);
                }
            }
        }
    }
}

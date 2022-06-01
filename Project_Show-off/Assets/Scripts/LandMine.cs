using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : MonoBehaviour
{
    [SerializeField] Vector3 force;
    [SerializeField] float radius;
    

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision");
        if (other.transform.TryGetComponent(out Player player))
        {
            Rigidbody body = player.GetComponent<Rigidbody>();
            body.AddForceAtPosition(force, this.transform.position);
        }
        else if (other.transform.TryGetComponent(out Projectile projectile))
        {
            Debug.Log("hit with bullet");
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.TryGetComponent(out Rigidbody body))
                {
                    body.AddForceAtPosition(force, this.transform.position);

                }
            }
        }
    }
}

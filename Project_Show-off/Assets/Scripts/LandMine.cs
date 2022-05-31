using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : MonoBehaviour
{
    [SerializeField] Vector3 force;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out Player player))
        {
            Rigidbody body = player.GetComponent<Rigidbody>();
            body.AddForceAtPosition(force, this.transform.position);
        }
    }
}

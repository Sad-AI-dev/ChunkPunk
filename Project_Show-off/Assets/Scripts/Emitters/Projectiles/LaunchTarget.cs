using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LaunchTarget : MonoBehaviour
{
    [SerializeField] Vector3 startVelocity;

    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.TransformDirection(startVelocity);
    }
}
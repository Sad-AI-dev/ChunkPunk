using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hittable : MonoBehaviour
{
    [SerializeField] UnityEvent<Player> onHit;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Projectile proj)) {
            onHit?.Invoke(proj.owner);
        }
    }
}

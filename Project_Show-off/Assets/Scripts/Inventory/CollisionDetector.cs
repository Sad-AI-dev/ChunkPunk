using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    List<GameObject> collisions;

    private void Start()
    {
        collisions = new List<GameObject>();
    }

    public bool HasCollisions()
    {
        return collisions.Count > 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!collisions.Contains(other.gameObject)) {
            collisions.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (collisions.Contains(other.gameObject)) {
            collisions.Remove(other.gameObject);
        }
    }
}

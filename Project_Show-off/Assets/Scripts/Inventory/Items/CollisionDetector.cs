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
        foreach (GameObject g in collisions) { Debug.Log(g.name); }
        return collisions.Count > 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Floor")) {
            if (!collisions.Contains(other.gameObject)) {
                collisions.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        collisions.Remove(other.gameObject);
    }

    private void OnDisable()
    {
        collisions = new List<GameObject>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionFx : MonoBehaviour
{
    [SerializeField] private float collideDelay = 1f;
    private bool canSFX = true;

    [SerializeField] private UnityEvent onCollidePlayer;

    private void Start()
    {
        canSFX = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (canSFX) {
            if (collision.transform.CompareTag("Player")) {
                onCollidePlayer?.Invoke();
                StartCoroutine(SFXCo());
            }
        }
    }

    IEnumerator SFXCo()
    {
        canSFX = false;
        yield return new WaitForSeconds(collideDelay);
        canSFX = true;
    }
}

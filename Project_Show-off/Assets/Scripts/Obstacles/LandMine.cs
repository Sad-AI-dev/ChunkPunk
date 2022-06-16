using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LandMine : MonoBehaviour
{
    [SerializeField] Vector3 force;
    [SerializeField] float radius;

    [Header("Player Hit Settings")]
    [SerializeField] float stunDuration = 1f;
    [SerializeField] float invinceDuration = 1.2f;
    [SerializeField] UnityEvent onExplode;

    [Header("Technical Timings")]
    [SerializeField] float startTime = 0.2f;
    bool starting = false;

    private void Start()
    {
        StartCoroutine(SetupCo());
    }
    IEnumerator SetupCo()
    {
        starting = true;
        yield return new WaitForSeconds(startTime);
        starting = false;
    }

    //----------------triggers--------------------------
    private void OnTriggerEnter(Collider other)
    {
        if (!starting && other.gameObject.CompareTag("Player")) {
            Explode();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!starting && collision.gameObject.CompareTag("Projectile")) {
            Explode();
        }
    }

    //--------------------trigger effect------------------------
    void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hitCollider in hitColliders) {
            EffectExternalObjects(hitCollider.gameObject);
        }
        onExplode?.Invoke();
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

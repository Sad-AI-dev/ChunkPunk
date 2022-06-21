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
    [SerializeField] float stunDelay = 0.2f;
    [SerializeField] float destroyDelay = 1f;
    bool triggered = false;

    private void Start()
    {
        triggered = false;
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
        if (!starting && !triggered && other.gameObject.CompareTag("Player")) {
            Explode();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!starting && !triggered && collision.gameObject.CompareTag("Projectile")) {
            Explode();
        }
    }

    //--------------------trigger effect------------------------
    void Explode()
    {
        triggered = true;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hitCollider in hitColliders) {
            EffectExternalObjects(hitCollider.gameObject);
        }
        onExplode?.Invoke();
        StartCoroutine(ExplodeCo(gameObject, destroyDelay));
    }

    private IEnumerator ExplodeCo(GameObject target, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(target);
    }

    void EffectExternalObjects(GameObject obj)
    {
        if (obj.CompareTag("Player")) {
            if (obj.TryGetComponent(out Player player)) {
                StartCoroutine(EffectPlayerCo(player, stunDelay));
            }
        } 
        else if (obj.CompareTag("Obstacle")) {
            if (obj != gameObject) {
                StartCoroutine(ExplodeCo(obj, Random.Range(0.1f, destroyDelay)));
            }
        }
    }

    IEnumerator EffectPlayerCo(Player player, float delay)
    {
        yield return new WaitForSeconds(delay);
        EffectPlayer(player);
    }

    void EffectPlayer(Player player)
    {
        //apply knockback + stun
        player.rb.velocity = Vector3.zero;
        player.rb.AddRelativeForce(force * 10, ForceMode.Impulse);
        //stun player
        player.getHit.StunPlayer(stunDuration, invinceDuration);
    }
}

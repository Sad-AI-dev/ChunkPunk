using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Banana : MonoBehaviour
{
    [SerializeField] private UnityEvent onHit;

    [Header("Effect Settings")]
    [SerializeField] float spinSpeed = 1;
    [SerializeField] float moveCoef = 1f;
    [SerializeField] [Range(0f, 1f)] float slowdownCoef = 0.8f;

    [Header("Timings")]
    [SerializeField] float effectTime = 1;
    [SerializeField] float invinceTime = 1.5f;

    [Header("Technical Timings")]
    [SerializeField] float setupTime = 0.5f;

    //states
    bool starting = false;

    private void Start()
    {
        StartCoroutine(SetupCo());
    }
    IEnumerator SetupCo()
    {
        starting = true;
        yield return new WaitForSeconds(setupTime);
        starting = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!starting && other.CompareTag("Player")) {
            if (other.TryGetComponent(out Player player)) {
                if (!player.getHit.alreadyHit) { //make sure player is not invincable
                    onHit?.Invoke();
                    SpinPlayer(player);
                }
            }
        }
    }

    void SpinPlayer(Player target)
    {
        //stun player
        target.getHit.StunPlayer(effectTime, invinceTime);
        StartCoroutine(SpinCo(target));
    }

    IEnumerator SpinCo(Player target)
    {
        //get start vars
        Vector3 startVelocity = target.rb.velocity;
        Quaternion startRot = target.characterModel.localRotation;
        //spin model
        while (target.isStunned) {
            target.characterModel.Rotate(Vector3.up * (spinSpeed * Time.deltaTime * (target.accelerate * moveCoef)));
            //velocity
            startVelocity *= slowdownCoef;
            target.rb.velocity = startVelocity;
            yield return null;
        }
        //reset model
        target.characterModel.localRotation = startRot;
    }
}
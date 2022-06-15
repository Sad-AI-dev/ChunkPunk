using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{
    [Header("Effect Settings")]
    [SerializeField] float spinSpeed = 1;
    [SerializeField] float moveCoef = 1f;

    [Header("Timings")]
    [SerializeField] float effectTime = 1;
    [SerializeField] float invinceTime = 1.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            if (other.TryGetComponent(out Player player)) {
                if (!player.getHit.alreadyHit) { //make sure player is not invincable
                    //player.Banana();
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
        //get start rot
        Quaternion startRot = target.characterModel.localRotation;
        //spin model
        while (target.isStunned) {
            target.characterModel.Rotate(Vector3.up * (spinSpeed * Time.deltaTime * (target.accelerate * moveCoef)));
            yield return null;
        }
        //reset model
        target.characterModel.localRotation = startRot;
    }
}
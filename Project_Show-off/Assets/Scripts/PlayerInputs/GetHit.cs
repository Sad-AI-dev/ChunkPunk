using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GetHit : MonoBehaviour
{
    [SerializeField] float X_ZAxisForce;
    [SerializeField] int bulletForce;
    [SerializeField] private int stunTime;
    [SerializeField] private float yKnockback;
    [SerializeField] private float invicibilityTime;
    [SerializeField] private ChangeTheFace faceChanger;
    //state
    [HideInInspector] public bool alreadyHit;

    //external components
    Player thisPlayer;

    //FX
    [Header("FX")]
    [SerializeField] private GameObject particleObj;
    [SerializeField] private UnityEvent onGetStunned;

    private void Awake()
    {
        thisPlayer = GetComponent<Player>();
        particleObj.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Projectile") && !alreadyHit)
        {
            GameObject projectile = collision.gameObject;
            Vector3 direction = projectile.transform.forward * X_ZAxisForce;
            direction.y = yKnockback;
            HitPlayer(direction.normalized);
            //stun the player
            StunPlayer(stunTime, invicibilityTime);
        }
    }

    private void HitPlayer(Vector3 bulletDir)
    {
        thisPlayer.rb.AddForce(bulletDir * bulletForce);
    }

    public void StunPlayer(float stunDuration, float invinceDuration)
    {
        alreadyHit = true;
        onGetStunned?.Invoke();
        StartCoroutine(StunCo(stunDuration));
        StartCoroutine(InvinceCo(invinceDuration));
    }

    private IEnumerator StunCo(float duration)
    {
        thisPlayer.isStunned = true;
        thisPlayer.stateController.stun?.Invoke();
        particleObj.SetActive(true);
        faceChanger.StunnedFace?.Invoke();
        yield return new WaitForSeconds(duration);
        faceChanger.NormalFace?.Invoke();
        particleObj.SetActive(false);
        thisPlayer.stateController.Skate?.Invoke();
        thisPlayer.isStunned = false;
    }

    private IEnumerator InvinceCo(float duration)
    {
        yield return new WaitForSeconds(duration);
        alreadyHit = false;
    }
}

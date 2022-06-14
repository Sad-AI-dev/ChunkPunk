using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BlindTrap : MonoBehaviour
{
    [SerializeField] float blindTime = 1f;
    [SerializeField] float fadeTime = 1f;

    [SerializeField] GameObject visuals;

    private CanvasGroup blindGroup;
    private float timer;
    //states
    private bool fading;
    bool activated;

    private void OnTriggerEnter(Collider other)
    {
        if (!activated && other.gameObject.CompareTag("Player")) {
            if (other.transform.TryGetComponent(out Player player)) {
                blindGroup = GetTargetGroup(player);
                GetTriggered();
            }
        }
    }
    CanvasGroup GetTargetGroup(Player target)
    {
        return PlayerManager.instance.playerUI[target.id - 1].blindGroup;
    }

    void GetTriggered()
    {
        activated = true;
        visuals.SetActive(false);
        StartCoroutine(StartFadeCo());
    }

    IEnumerator StartFadeCo()
    {
        blindGroup.alpha = 1;
        timer = 0;
        fading = false;
        yield return new WaitForSeconds(blindTime); //stay fully blind for duration
        fading = true;
    }

    private void Update()
    {
        if(fading) {
            timer += Time.deltaTime;
            blindGroup.alpha = 1 - (timer / fadeTime);
            if (timer > fadeTime) { End(); }
        }
    }

    void End()
    {
        Destroy(gameObject);
    }
}

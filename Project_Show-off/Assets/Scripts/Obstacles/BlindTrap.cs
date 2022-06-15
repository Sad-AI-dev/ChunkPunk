using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BlindTrap : MonoBehaviour
{
    [SerializeField] float setupTime = 1f;
    [Header("trap settings")]
    [SerializeField] float blindTime = 1f;
    [SerializeField] float fadeinTime = 0.5f;
    [SerializeField] float fadeTime = 1f;

    [SerializeField] GameObject visuals;

    private CanvasGroup blindGroup;
    private float timer;
    //states
    private bool fading;
    bool fadingIn = true;
    bool activated;

    bool starting;

    Transform owner;

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
        if (starting) {
            if (other.CompareTag("Player")) {
                owner = other.transform;
            }
        }
        else if (!activated && other.gameObject.CompareTag("Player")) {
            if (other.transform == owner) { return; }
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

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == owner) { owner = null; }
    }

    //----------------------trigger trap---------------------
    void GetTriggered()
    {
        activated = true;
        visuals.SetActive(false);
        StartCoroutine(StartFadeCo());
    }

    IEnumerator StartFadeCo()
    {
        blindGroup.alpha = 0;
        timer = 0;
        fading = true;
        yield return new WaitForSeconds(fadeinTime);
        fading = false;
        yield return new WaitForSeconds(blindTime); //stay fully blind for duration
        fading = true;
    }

    private void Update()
    {
        if(fading) {
            timer += Time.deltaTime;
            if (fadingIn) { FadeIn(); }
            else { FadeOut(); }
        }
    }

    //----------------fades--------------
    void FadeIn()
    {
        blindGroup.alpha = timer / fadeinTime;
        if (timer > fadeinTime) {
            timer = 0;
            blindGroup.alpha = 1;
            fadingIn = !fadingIn;
        }
    }

    void FadeOut()
    {
        blindGroup.alpha = 1 - (timer / fadeTime);
        if (timer > fadeTime) { End(); }
    }


    void End()
    {
        Destroy(gameObject);
    }
}

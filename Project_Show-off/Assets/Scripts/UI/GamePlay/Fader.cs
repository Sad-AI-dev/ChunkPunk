using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fader : MonoBehaviour
{
    private CanvasGroup group;
    private float timer;

    //states
    private bool activated;
    private bool fading;
    private bool fadingIn;

    //settings
    private float fadeinTime;
    private float blindTime;
    private float fadeTime;

    //events
    [SerializeField] private UnityEvent onBlind;

    private void Start()
    {
        group = GetComponent<CanvasGroup>();
        End(); //reset vars
    }

    public void Fade(float fadeinTime, float blindTime, float fadeTime)
    {
        if (!activated) {
            this.fadeinTime = fadeinTime;
            this.blindTime = blindTime;
            this.fadeTime = fadeTime;
            //update states
            activated = true;
            //start fade
            onBlind?.Invoke();
            StartCoroutine(StartFadeCo());
        }
    }

    IEnumerator StartFadeCo()
    {
        group.alpha = 0;
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
        group.alpha = timer / fadeinTime;
        if (timer > fadeinTime) {
            timer = 0;
            group.alpha = 1;
            fadingIn = !fadingIn;
        }
    }

    void FadeOut()
    {
        group.alpha = 1 - (timer / fadeTime);
        if (timer > fadeTime) { End(); }
    }

    private void End()
    {
        timer = 0;
        fadingIn = true;
        fading = false;
        activated = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BlindTrap : MonoBehaviour
{
    [SerializeField] float setupTime = 0.1f;
    [Header("trap settings")]
    [SerializeField] float blindTime = 1f;
    [SerializeField] float fadeinTime = 0.5f;
    [SerializeField] float fadeTime = 1f;

    [SerializeField] private UnityEvent onBlind;

    [Header("Technical")]
    [SerializeField] GameObject visuals;

    private Fader fader;

    bool starting;

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
        if (!starting && other.gameObject.CompareTag("Player")) {
            if (other.transform.TryGetComponent(out Player player)) {
                fader = GetTargetFader(player);
                GetTriggered();
            }
        }
    }
    Fader GetTargetFader(Player target)
    {
        return PlayerManager.instance.playerUI[target.id - 1].fader;
    }

    //----------------------trigger trap---------------------
    void GetTriggered()
    {
        fader.Fade(fadeinTime, blindTime, fadeTime);
        Destroy(gameObject);
    }
}

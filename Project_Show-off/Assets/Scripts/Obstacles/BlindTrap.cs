using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BlindTrap : MonoBehaviour
{
    [SerializeField] float timeToRemove;
    [SerializeField] float perSecondClearSpeed;
    private CanvasGroup theBlind;
    private bool isFaded;
    private float timer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out Player player))
        {
            Debug.Log("blinded");
            theBlind = PlayerManager.instance.playerUI[player.id - 1].blindGroup;
            theBlind.alpha = 1;
            isFaded = true;
            timer = 0;
           // StartCoroutine(removeBlind(theBlind));
            
            
        }
    }

    private void Update()
    {
        if(isFaded)
        {
            timer += Time.deltaTime;
            theBlind.alpha = 1 - (timer / timeToRemove);
            
        }
    }
}

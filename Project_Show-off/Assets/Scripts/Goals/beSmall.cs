using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beSmall : MonoBehaviour
{
    private float thisTransform;
    [SerializeField] float endSize;
    [SerializeField] private float timeToSmall;
     private Vector3 goal;
    private float currentTime;
    private void Awake()
    {
        goal = this.transform.localScale;
        StartCoroutine(makeSmall());
        currentTime = 0;
    }


    private IEnumerator makeSmall()
    {
        while(currentTime < timeToSmall)
        {
            currentTime += Time.deltaTime ;
            float duration =  this.transform.localScale.x - endSize ;
            thisTransform = goal.x - ((currentTime  / timeToSmall) * duration);
            transform.localScale = new Vector3(thisTransform, transform.localScale.y, thisTransform);
            yield return new WaitForSeconds(0.5f);
        }



    }
}

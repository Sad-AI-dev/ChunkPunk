using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beSmall : MonoBehaviour
{
    [SerializeField] private Transform target;

    private float startSize;
    [SerializeField] private float endSize;
    [SerializeField] private float duration;

    private float diff;
    private float timer;

    private void Awake()
    {
        timer = 0;
        startSize = target.localScale.x;
        diff = Mathf.Abs(startSize - endSize);
        StartCoroutine(MakeSmall());
    }

    private IEnumerator MakeSmall()
    {
        while(timer < duration) {
            timer += Time.deltaTime;
            float size = startSize - ((timer / duration) * diff);
            target.localScale = new Vector3(size, target.localScale.y, size);
            //wait frame
            yield return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveTheRect : MonoBehaviour
{
    [SerializeField] Transform A;
    [SerializeField] Transform B;
    RectTransform thisObject;
    bool where;
    private void Awake()
    {
        thisObject = GetComponent<RectTransform>();
        
    }

    public void move()
    {
        if (where)
        {
            thisObject.localPosition = B.position;
        } else if (!where)
        {
            thisObject.localPosition = A.position;
        }
    }

}

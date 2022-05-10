using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoint : MonoBehaviour
{
    private bool isTriggered;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && isTriggered == false)
        {
            checkPointManager.instance.lastCheckPoint = other.transform;
            Debug.Log("New checkpoint is " + other.transform);
            isTriggered = true;
        }
    }
}

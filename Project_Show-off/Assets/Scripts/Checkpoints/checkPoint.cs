using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("New checkpoint is " + other.transform);
            Player currentPlayer = other.GetComponent<Player>();
            checkPointManager.instance.allPlayerCheckPoints[currentPlayer.id] = other.transform;
            Debug.Log(checkPointManager.instance.allPlayerCheckPoints); 
        }
    }
}

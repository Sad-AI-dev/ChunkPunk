using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoint : MonoBehaviour
{
    private bool isInside;
    int indexer = 0;    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && isInside == false)
        {
            indexer++;
            //Debug.Log(indexer);
            //Debug.Log("New checkpoint is " + other.transform);
            Player currentPlayer = other.GetComponent<Player>();
            
            checkPointManager.instance.allPlayerCheckPoints[currentPlayer.id] = transform;
            //Debug.Log(checkPointManager.instance.allPlayerCheckPoints[currentPlayer.id].position);
            isInside = true;
            //Debug.Log(isInside);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        isInside = false;
        //Debug.Log(isInside);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPointManager : MonoBehaviour
{
    public Transform lastCheckPoint;
    public Dictionary<int, Transform> allPlayerCheckPoints = new Dictionary<int, Transform>();
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    public static checkPointManager instance;

    private void Update()
    {
        foreach (KeyValuePair<int, Transform> kvp in allPlayerCheckPoints)
        {
            //Debug.LogFormat("Item: {0} - {1}g", kvp.Key, kvp.Value);
        }

        
    }

}

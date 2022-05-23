using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPointManager : MonoBehaviour
{
    [SerializeField] public Dictionary<Player, Transform> allPlayerCheckPoints = new Dictionary<Player, Transform>();
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
        foreach (KeyValuePair<Player, Transform> kvp in allPlayerCheckPoints)
        {
            //Debug.LogFormat("Item: {0} - {1}g", kvp.Key, kvp.Value);
        }

        
    }

}

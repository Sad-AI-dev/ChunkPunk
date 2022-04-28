using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    //----------------singleton-----------------
    private void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
    }
    public static GoalManager instance;

    //-------------------vars------------------------
    [SerializeField] Transform goalPointHolder;
    readonly List<Vector3> goalPositions = new List<Vector3>();

    [Header("Technical settings")]
    [SerializeField] float minimumDistance;

    private void Start()
    {
        foreach (Transform child in goalPointHolder) {
            goalPositions.Add(child.position);
        }
    }

    public Vector3 GetBestSpawnPos()
    {
        
    }


}
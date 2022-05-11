using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleStream : MonoBehaviour, IObstacle
{
    [Tooltip("amount of seconds between each object being spawned")]
    [SerializeField] float spawnDelay;

    [Header("Technical settings")]
    [SerializeField] GameObject prefab;
    List<Transform> objPool = new();
    [SerializeField] Transform pathHolder;
    List<Transform> path = new();

    bool executing = false;

    void Start()
    {
        foreach (Transform child in pathHolder) { //compile path
            path.Add(child);
        }
    }
    void Update()
    {

    }

    public void Execute()
    {
        if (executing) { End(); }
        else { StartMove(); }
        executing = !executing;
    }

    void StartMove()
    {

    }

    public void End()
    {

    }

    //--------------move objects-----------

    //-------------data----------
    private class Moveable
    {
        public Transform body;
        int target = 0;

        public void Move()
        {

        }
    }

}
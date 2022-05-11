using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour, IObstacle
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float startInterval = 1f;

    //modes
    [SerializeField] bool isLooping;
    bool goingForward = true;
    bool executed = false;

    [Header("Techinical Settings")]
    [SerializeField] Transform moveable;
    int moveTarget = 0;
    [SerializeField] Transform pathHolder;
    List<Transform> path = new();

    void Start()
    {
        foreach (Transform child in pathHolder) {
            path.Add(child);
        }
    }

    void Update()
    {
        if (executed) {
            Move();
        }
    }

    void Move()
    {
        moveable.position = Vector3.MoveTowards(moveable.position, path[moveTarget].position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(moveable.position, path[moveTarget].position) < 0.2f) {
            OnReachTarget();
        }
    }
    void OnReachTarget()
    {
        moveTarget += goingForward ? 1 : -1;
        if (moveTarget >= path.Count) {
            goingForward = false;
            moveTarget = path.Count - 1; //set target to be 
        }
    }

    //-------------------start / end----------------------
    public void Execute()
    {
        if (executed) { End(); }
        else { StartMove(); }
        executed = !executed;
    }

    void StartMove()
    {

    }

    public void End()
    {

    }
}

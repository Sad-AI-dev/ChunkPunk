using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour, IObstacle
{
    [SerializeField] float moveSpeed = 1f;

    //modes
    [System.Serializable] enum Mode {
        single,
        looping
    }
    [SerializeField] Mode mode;

    bool executing = false;
    bool moving = false;
    bool movingForward = true;

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
        moveable.position = path[0].position; //set moveable to start pos;
    }

    void Update()
    {
        if (moving) {
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
        moveTarget += movingForward ? 1 : -1;
        if (moveTarget >= path.Count) { OnReachEnd(); }
        else if (moveTarget < 0) { OnReachStart(); }
    }

    void OnReachEnd()
    {
        movingForward = false;
        moveTarget = path.Count - 1;
        if (mode == Mode.single) {
            moving = false;
        }
    }
    void OnReachStart()
    {
        movingForward = true;
        moveTarget = 0;
        moving = executing; //stop if ended. otherwise, continue
    }

    //-------------------start / end----------------------
    public void Execute()
    {
        if (executing) { End(); }
        else { StartMove(); }
        executing = !executing;
    }

    void StartMove()
    {
        moving = true;
    }

    public void End()
    {
        movingForward = false;
        if (mode == Mode.single) {
            moving = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour, IObstacle
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float onReachEndDelay = 0f;

    //modes
    [System.Serializable] enum Mode {
        single,
        looping
    }
    [SerializeField] Mode mode;
    [SerializeField] bool rotate = true;

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
        if (Vector3.Distance(moveable.position, path[moveTarget].position) < 0.01f) {
            OnReachTarget();
        }
    }

    void OnReachTarget()
    {
        moveTarget += movingForward ? 1 : -1;
        if (moveTarget >= path.Count || moveTarget < 0) {
            OnReachPathEdge();
        }
        if (rotate) moveable.LookAt(path[moveTarget]); //update rotation
    }

    void OnReachPathEdge()
    {
        movingForward = !movingForward; //turn around
        moving = false; //stop moving
        moveTarget = moveTarget < 0 ? 0 : path.Count - 1; //set target
        //on reach end delay
        if (executing && mode == Mode.looping) {
            StartCoroutine(ReachPathEdgeCo());
        }
    }

    IEnumerator ReachPathEdgeCo()
    {
        yield return new WaitForSeconds(onReachEndDelay);
        moving = true;
    }

    //-------------------start / end----------------------
    public void Execute()
    {
        if (executing) { End(); }
        executing = !executing;
        moving = true;
    }

    public void End()
    {
        //move back to start
        movingForward = false;
    }
}

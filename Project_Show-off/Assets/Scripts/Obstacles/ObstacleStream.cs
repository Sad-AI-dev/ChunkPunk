using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleStream : MonoBehaviour, IObstacle
{
    [Tooltip("amount of seconds between each object being spawned")]
    [SerializeField] float spawnDelay;
    [SerializeField] float moveSpeed;
    [SerializeField] bool rotate = true;

    [Header("Technical settings")]
    [SerializeField] GameObject prefab;
    List<Moveable> objPool = new();
    [SerializeField] Transform pathHolder;
    protected readonly List<Transform> path = new();

    bool executing = false;

    void Start()
    {
        foreach (Transform child in pathHolder) { //compile path
            path.Add(child);
        }
    }

    public void Execute()
    {
        if (executing) { End(); }
        else { StartMove(); }
        executing = !executing;
    }

    void StartMove()
    {
        StartCoroutine(SpawnCo());
    }

    public void End()
    {

    }

    //---------------------main loops-----------------
    void Update()
    {
        if (IsMoving()) {
            Move();
        }
    }

    IEnumerator SpawnCo()
    {
        InitializeMover(GetAvailableMover());
        yield return new WaitForSeconds(spawnDelay);
        if (executing) { StartCoroutine(SpawnCo()); }
    }

    //--------------move objects-----------
    void Move()
    {
        foreach (Moveable toMove in objPool) {
            if (toMove.moving) {
                MoveObject(toMove);
            }
        }
    }

    void MoveObject(Moveable toMove)
    {
        toMove.body.position = Vector3.MoveTowards(toMove.body.position, path[toMove.target].position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(toMove.body.position, path[toMove.target].position) < 0.01f) {
            OnReachTarget(toMove);
        }
    }

    void OnReachTarget(Moveable toMove)
    {
        toMove.target++;
        if (toMove.target >= path.Count) {
            //reset object
            ResetObject(toMove);
        }
        toMove.body.LookAt(path[toMove.target]);
    }

    //-------------------reset objects
    void InitializeMover(Moveable toMove)
    {
        toMove.moving = true;
        toMove.body.gameObject.SetActive(true);
    }

    void ResetObject(Moveable toMove)
    {
        toMove.target = 0;
        toMove.moving = false;
        toMove.body.position = path[0].position;
        toMove.body.gameObject.SetActive(false);
    }
    //-------------data----------
    private class Moveable
    {
        public Transform body;
        public int target = 0;

        public bool moving = false;
    }

    bool IsMoving()
    {
        foreach (Moveable mover in objPool) {
            if (mover.moving) return true;
        }
        return false;
    }

    Moveable GetAvailableMover()
    {
        foreach (Moveable mover in objPool) {
            //try grab existing object from pool
            if (!mover.moving) { return mover; }
        }
        //create new object and add to pool
        objPool.Add(CreateNewMover());
        return objPool[^1];
    }

    Moveable CreateNewMover()
    {
        Transform t = Instantiate(prefab).transform;
        Moveable newMover = new Moveable { body = t };
        newMover.body.position = path[0].position; //set start position
        return newMover;
    }
}
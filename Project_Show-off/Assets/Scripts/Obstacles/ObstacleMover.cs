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
    [SerializeField] GameObject moveablePrefab;
    List<Moveable> moveables = new();
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
            //MOVE
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

    //-------------------data--------------
    private struct Moveable
    {
        public Transform t;
        public int target;
    }
}

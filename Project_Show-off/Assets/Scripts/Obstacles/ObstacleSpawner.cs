using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour, IObstacle
{
    [SerializeField] GameObject obstaclePrefab;
    [Header("spawn locations")]
    [SerializeField] Transform spawnPointHolder;
    List<Transform> spawnPoints = new();

    List<GameObject> createdObjects = new();
    bool executed = false;

    void Start()
    {
        foreach (Transform child in spawnPointHolder) { //load spawn points
            spawnPoints.Add(child);
        }
    }

    public void Execute()
    {
        if (executed) { End(); }
        else { SpawnObjects(); }
        executed = !executed;
    }

    void SpawnObjects()
    {
        if (createdObjects.Count == 0) { //first time trap is trigger, instantiate new objects
            foreach (Transform point in spawnPoints) {
                createdObjects.Add(Instantiate(obstaclePrefab, point));
            }
        }
        else { //reuse existing objects
            foreach (GameObject obj in createdObjects) {
                obj.SetActive(true);
            }
        }
    }

    public void End()
    {
        foreach (GameObject obj in createdObjects) {
            obj.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    readonly List<Vector3> goalPositions = new();

    public readonly List<Transform> players = new();

    [Header("Technical settings")]
    [SerializeField] GameObject goalPrefab;
    [SerializeField] float minimumDistance;

    private void Start()
    {
        //store all goal points
        foreach (Transform child in goalPointHolder) {
            goalPositions.Add(child.position);
        }
    }

    public Vector3 GetBestSpawnPos()
    {
        goalPositions.Sort(SortByBestPosition);
        return goalPositions[0];
    }

    //-----------------------goal creation-------------------
    public Goal SpawnGoal()
    {
        GameObject g = Instantiate(goalPrefab);
        g.transform.position = GetBestSpawnPos();
        return g.GetComponent<Goal>();
    }

    //----------------------custom sort------------------------
    int SortByBestPosition(Vector3 a, Vector3 b)
    { //check for minimum distance
        foreach (Transform t in players) {
            if (Vector3.Distance(a, t.position) < minimumDistance &&
                Vector3.Distance(b, t.position) > minimumDistance) {
                return 1; //push back
            }
        }
        //find comparitive distance
        return GetComparitiveDistance(a).CompareTo(GetComparitiveDistance(b));
    } //-1 means good, +1 means bad (consider it a shift in index)

    float GetComparitiveDistance(Vector3 point)
    {
        float[] distances = new float[players.Count];
        for (int i = 0; i < distances.Length; i++) {
            distances[i] = Vector3.Distance(point, players[i].position);
        }
        float partion = distances[0] / (distances[0] + distances[1]); //number between 0 and 1
        return Mathf.Abs(partion - 0.5f); //lower is better!
    }
}
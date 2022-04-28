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
    readonly List<Vector3> goalPositions = new();

    public readonly List<Transform> players = new();

    [Header("Technical settings")]
    [SerializeField] GameObject goalPrefab;
    [SerializeField] float minimumDistance;

    private void Start()
    {
        foreach (Transform child in goalPointHolder) {
            goalPositions.Add(child.position);
        }

        //TEST
        GameObject g = Instantiate(goalPrefab);
        g.transform.position = GetBestSpawnPos();
        Debug.Log(g.transform.position);
    }

    public Vector3 GetBestSpawnPos()
    {
        goalPositions.Sort(SortByBestPosition);
        return goalPositions[0];
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
    }
    //-1 means good, +1 means bad (consider it a shift in index)

    float GetComparitiveDistance(Vector3 point)
    {
        List<float> distances = new();
        foreach (Transform t in players) {
            distances.Add(Vector3.Distance(point, t.position)); //find distances to players
        }
        return Mathf.Abs((distances[0] / (distances[0] + distances[1])) - 0.5f);
    }
}
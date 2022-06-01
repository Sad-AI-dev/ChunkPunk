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
    [SerializeField] NodeGraph graph;

    [Header("Technical settings")]
    [SerializeField] GameObject goalPrefab;
    public int minimumDistance;

    //-----------------------goal creation-------------------
    public Goal SpawnGoal()
    {
        GameObject g = Instantiate(goalPrefab);
        g.transform.position = graph.GetBestGoalPos();
        return g.GetComponent<Goal>();
    }
}
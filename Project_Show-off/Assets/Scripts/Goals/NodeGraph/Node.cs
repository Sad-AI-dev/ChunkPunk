using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] List<Node> neighbours = new();

    public Dictionary<Player, int> steps = new();
    [HideInInspector] public int compareDistance = 0;

    public void SetSteps(Player p, int stepCount)
    {
        if (steps.ContainsKey(p)) {
            steps[p] = stepCount;
        }
        else { steps.Add(p, stepCount); }

        //record compare distance
        if (steps.Keys.Count > 1) {
            compareDistance = GetCompareValue();
        }
    }

    public List<Node> GetNeighbours()
    {
        return neighbours;
    }

    //-------------------------custom sort----------------------------------
    int GetCompareValue()
    {
        List<int> input = new List<int>(steps.Values);
        foreach (int i in input) { //min distance check
            if (i <= GoalManager.instance.minimumDistance) return 100;
        }
        return GetLargestDeviator(input, GetLowest(input));
    }

    int GetLowest(List<int> list)
    {
        int lowest = list[0];
        for (int i = 1; i < list.Count; i++) {
            if (list[i] < lowest) { lowest = list[i]; }
        }
        return lowest;
    }

    int GetLargestDeviator(List<int> list, int normal)
    {
        int largest = 0;
        foreach (int i in list) {
            int deviator = Mathf.Abs(normal - i);
            if (deviator > largest) largest = deviator;
        }
        return largest;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] List<Node> neighbours = new();

    public Dictionary<Player, int> steps = new();

    public void SetSteps(Player p, int stepCount)
    {
        if (steps.ContainsKey(p)) {
            steps[p] = stepCount;
        }
        else { steps.Add(p, stepCount); }
    }

    public List<Node> GetNeighbours()
    {
        return neighbours;
    }

    //-------------------------custom sort----------------------------------
    public int ComparativeSort(Node a, Node b, int minDistance)
    {
        int valueA = GetCompareValue(a, minDistance);
        int valueB = GetCompareValue(b, minDistance);
        //return result
        if (valueA < 0 || valueB < 0) { //no input or inside of minDistance
            return -valueA.CompareTo(valueB);
        }
        else {
            return valueA.CompareTo(valueB);
        }
    }

    public int GetCompareValue(Node node, int minDistance)
    {
        List<int> input = new List<int>(node.steps.Values);
        if (input.Count > 1) {
            foreach (int i in input) {//min distance check
                if (i <= minDistance) return -1;
            }
            return GetLargestDeviator(input, GetAverage(input));
        }
        return -1;
    }

    int GetAverage(List<int> list)
    {
        int output = 0;
        foreach (int i in list) {
            output += i;
        }
        return output / list.Count;
    }

    int GetLargestDeviator(List<int> list, int average)
    {
        int largest = 0;
        foreach (int i in list) {
            int deviator = Mathf.Abs(average - i);
            if (deviator > largest) largest = deviator;
        }
        return largest;
    }
}
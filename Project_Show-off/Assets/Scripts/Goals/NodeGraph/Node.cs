using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] List<Node> neighbours = new();

    public Dictionary<Player, int> steps = new();

    public void RecursiveSearch(Player searcher, List<Node> searchedNodes, int stepsTaken)
    {
        SetSteps(searcher, stepsTaken);
        searchedNodes.Add(this);
        stepsTaken++;

        foreach (Node n in neighbours) {
            if (!searchedNodes.Contains(n)) {
                n.RecursiveSearch(searcher, new List<Node>(searchedNodes), stepsTaken);
            }
        }
    }

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
}
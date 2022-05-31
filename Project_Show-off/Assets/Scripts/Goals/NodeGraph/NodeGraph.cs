using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGraph : MonoBehaviour
{
    public List<Node> nodes = new List<Node>();
    List<Node> searchedNodes;

    //TEST
    private void Start()
    {
        //GetBestGoalPos(1);
    }

    public Node GetClosestNode(Vector3 pos)
    {
        Node closest = nodes[0];
        float minDist = Vector3.Distance(nodes[0].transform.position, pos);
        //check all nodes
        for (int i = 1; i < nodes.Count; i++) {
            float checkDistance = Vector3.Distance(nodes[i].transform.position, pos);
            if (checkDistance < minDist) {
                closest = nodes[i];
                minDist = checkDistance;
            }
        }
        return closest;
    }

    public Vector3 GetBestGoalPos()
    {
        foreach (Player p in PlayerManager.instance.players) {
            StartBreadthSearch(p, GetClosestNode(p.transform.position));
        }
        //find best positions
        nodes.Sort((Node a, Node b) => a.compareDistance.CompareTo(b.compareDistance));
        return nodes[0].transform.position;
    }

    //-------------------------breadth first search---------------------------
    void StartBreadthSearch(Player p, Node startPoint)
    {
        startPoint.SetSteps(p, 0);
        searchedNodes = new List<Node> { startPoint };
        //start search
        BreadthSearch(p, 1, startPoint);
    }

    void BreadthSearch(Player p, int steps, Node currentNode)
    {
        //set steps
        List<Node> toSearch = new List<Node>();
        foreach (Node n in currentNode.GetNeighbours()) {
            if (!searchedNodes.Contains(n)) {
                n.SetSteps(p, steps);
                searchedNodes.Add(n);
                toSearch.Add(n);
            }
        }
        //pass on search
        foreach (Node n in toSearch) {
            BreadthSearch(p, steps + 1, n);
        }
    }
}
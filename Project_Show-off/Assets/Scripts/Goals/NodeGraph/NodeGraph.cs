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
            BreadthSearch(p, GetClosestNode(p.transform.position));
        }
        //find best positions
        nodes.Sort((Node a, Node b) => a.compareDistance.CompareTo(b.compareDistance));
        return nodes[0].transform.position;
    }

    //-------------------------breadth first search---------------------------
    void BreadthSearch(Player p, Node startPoint)
    {
        int steps = 0;
        Queue<Node> registerQueue = new Queue<Node>( new[] { startPoint } );
        Queue<Node> nextStepQueue = new Queue<Node>();
        List<Node> searchedNodes = new List<Node>();

        while (registerQueue.Count > 0) {
            Node n = registerQueue.Peek();
            //register node
            n.SetSteps(p, steps);
            searchedNodes.Add(n);
            registerQueue.Dequeue();
            //register next steps
            foreach (Node neighbour in n.GetNeighbours()) {
                if (!searchedNodes.Contains(neighbour)) {
                    nextStepQueue.Enqueue(neighbour);
                }
            }
            //next step check
            if (registerQueue.Count <= 0 && nextStepQueue.Count > 0) {
                steps++;
                registerQueue = new Queue<Node>(nextStepQueue);
                nextStepQueue.Clear(); //reset next step queue
            }
        }
    }
}
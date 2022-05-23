using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGraph : MonoBehaviour
{
    public List<Node> nodes = new List<Node>();

    public Node GetClosesNode(Vector3 pos)
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
            GetClosesNode(p.transform.position).RecursiveSearch(p, new List<Node>(), 0);
        }

        //find best positions
        return Vector3.zero;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Node))]
public class NodeEditor : Editor
{
    private void OnSceneGUI()
    {
        Node node = target as Node;

        foreach (Node n in node.GetNeighbours()) {
            Handles.DrawLine(n.transform.position, node.transform.position);
        }
    }
}
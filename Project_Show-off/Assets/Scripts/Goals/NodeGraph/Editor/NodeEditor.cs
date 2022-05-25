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

        Handles.color = Color.green;
        Handles.DrawSolidDisc(node.transform.position, Vector3.up, 0.5f);

        Handles.color = Color.white;
        foreach (Node n in node.GetNeighbours()) {
            if (n) Handles.DrawLine(n.transform.position, node.transform.position);
        }
    }
}
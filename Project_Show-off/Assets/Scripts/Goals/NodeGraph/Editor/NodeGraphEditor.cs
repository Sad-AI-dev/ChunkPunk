using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NodeGraph))]
public class NodeGraphEditor : Editor
{
    private void OnSceneGUI()
    {
        NodeGraph graph = target as NodeGraph;
        if (graph.nodes.Count != graph.transform.childCount) { CompileNodes(graph); }

        List<Node> linkedNodes = new();
        foreach (Node node in graph.nodes) {
            linkedNodes.Add(node);
            Vector3 nodePos = node.transform.position;
            foreach (Node neighbour in node.GetNeighbours()) {
                if (!linkedNodes.Contains(neighbour)) {
                    Handles.DrawLine(nodePos, neighbour.transform.position);
                }
            }
        }
    }

    void CompileNodes(NodeGraph graph)
    {
        graph.nodes.Clear();
        foreach (Transform t in graph.transform) {
            if (t.TryGetComponent(out Node node)) {
                graph.nodes.Add(node);
            }
        }
    }
}
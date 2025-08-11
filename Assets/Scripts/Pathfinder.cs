using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] private NodeBehavior startNode;
    [SerializeField] private NodeBehavior targetNode;
    private List<NodeBehavior> path;
    
    public void StartPathfinding()
    {
        path = startNode.FindPath(targetNode);
        PrintPath();
        HighlightNodes();
        if (path != null)
        {
            DrawConnections(path);
        }
    }
    
    private void PrintPath()
    {
        if (path == null || path.Count == 0)
        {
            Debug.Log("No path found.");
            return;
        }

        string pathString = "Path: ";
        foreach (var node in path)
        {
            pathString += node.name + " -> ";
        }
        Debug.Log(pathString);
    }
    
    private void HighlightNodes()
    {
        if (path == null) return;
        foreach (var node in path)
        {
            var renderer = node.gameObject.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.red;
            }
        }
    }

    private void DrawConnections(List<NodeBehavior> nodes)
    {
        if (nodes == null || nodes.Count < 2) return;
        for (int i = 0; i < nodes.Count - 1; i++)
        {
            var lineObj = new GameObject("ConnectionLine");
            lineObj.transform.parent = this.transform;
            var lineRenderer = lineObj.AddComponent<LineRenderer>();
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.red;
            lineRenderer.sortingOrder = 2;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, nodes[i].transform.position);
            lineRenderer.SetPosition(1, nodes[i + 1].transform.position);
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
        }
    }
}

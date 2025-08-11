using System;
using System.Collections.Generic;
using UnityEngine;

public class NodeBehavior : MonoBehaviour
{
    public NodeBehavior[] connectedNodes;
    
    private Queue<NodeBehavior> nodeQueue;
    private List<NodeBehavior> visitedNodes;
    private List<NodeBehavior> path;

    private void Start()
    {
        DrawConnections();
    }

    public List<NodeBehavior> FindPath(NodeBehavior targetNode)
    {
        nodeQueue = new Queue<NodeBehavior>();
        visitedNodes = new List<NodeBehavior>();
        var parents = new Dictionary<NodeBehavior, NodeBehavior>();
        nodeQueue.Enqueue(this);
        visitedNodes.Add(this);

        while (nodeQueue.Count > 0)
        {
            NodeBehavior currentNode = nodeQueue.Dequeue();
            if (currentNode == targetNode)
            {
                // Reconstruct path
                path = new List<NodeBehavior>();
                NodeBehavior node = targetNode;
                while (node != null)
                {
                    path.Insert(0, node);
                    parents.TryGetValue(node, out node);
                }
                return path;
            }

            foreach (NodeBehavior neighbor in currentNode.connectedNodes)
            {
                if (visitedNodes.Contains(neighbor)) continue;
                nodeQueue.Enqueue(neighbor);
                visitedNodes.Add(neighbor);
                parents[neighbor] = currentNode;
            }
        }
        Debug.Log("No path found to the target node.");
        return null; // No path found
    }
    private void DrawConnections()
    {
        foreach (var connectedNode in connectedNodes)
        {
            var lineObj = new GameObject("ConnectionLine");
            lineObj.transform.parent = this.transform;
            var lineRenderer = lineObj.AddComponent<LineRenderer>();
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.startColor = Color.white;
            lineRenderer.endColor = Color.white;
            lineRenderer.sortingOrder = 1;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, this.transform.position);
            lineRenderer.SetPosition(1, connectedNode.transform.position);
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
        }
    }
}

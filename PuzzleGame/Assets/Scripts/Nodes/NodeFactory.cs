using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// Factory class for creating nodes.
/// </summary>
public class NodeFactory
{
    private readonly GameObject nodePrefab;
    private readonly Color[] nodeColors;

    public NodeFactory(GameObject prefab, Color[] colors)
    {
        nodePrefab = prefab;
        nodeColors = colors;
    }

    /// <summary>
    /// Creates a new node at the specified position and parent transform.
    /// </summary>
    /// <param name="position">Position of the node.</param>
    /// <param="parent">Parent transform for the node.</param>
    /// <returns>The created node.</returns>
    public NodeClass CreateNode(Vector3 position, Transform parent)
    {
        GameObject nodeObject = Object.Instantiate(nodePrefab, position, Quaternion.identity, parent);
        NodeClass node = nodeObject.GetComponent<NodeClass>();
        NodeType randomType = GetRandomNodeType();
        Color randomColor = GetRandomColor();
        node.Initialize(randomType, randomColor);
        return node;
    }

    private NodeType GetRandomNodeType()
    {
        NodeType[] nodeTypes = (NodeType[])System.Enum.GetValues(typeof(NodeType));
        return nodeTypes[Random.Range(0, nodeTypes.Length)];
    }

    private Color GetRandomColor()
    {
        return nodeColors[Random.Range(0, nodeColors.Length)];
    }
}

using UnityEngine;

/// <summary>
/// Factory class for creating nodes.
/// </summary>
public class NodeFactory
{
    private readonly GameObject nodePrefab;

    public NodeFactory(GameObject prefab)
    {
        nodePrefab = prefab;
    }

    /// <summary>
    /// Creates a new node at the specified position and parent transform.
    /// </summary>
    /// <param name="type">Type of the node.</param>
    /// <param name="position">Position of the node.</param>
    /// <param name="parent">Parent transform for the node.</param>
    /// <returns>The created node.</returns>
    public GameObject CreateNode(NodeType type, Vector3 position, Transform parent)
    {
        GameObject nodeObject = Object.Instantiate(nodePrefab, position, Quaternion.identity, parent);
        NodeClass node = nodeObject.GetComponent<NodeClass>();
        if (node != null)
        {
            node.Initialize(type);
        }
        return nodeObject;
    }
}

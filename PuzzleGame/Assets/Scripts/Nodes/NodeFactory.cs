using UnityEngine;

/// <summary>
/// Factory class for creating nodes.
/// </summary>
public class NodeFactory
{
    private readonly GameObject nodePrefab;

    public NodeFactory(GameObject nodePrefab)
    {
        this.nodePrefab = nodePrefab;
    }

    /// <summary>
    /// Creates a node of the specified type at the specified position under the specified parent.
    /// </summary>
    /// <param name="type">Type of the node.</param>
    /// <param name="position">Position of the node.</param>
    /// <param name="parent">Parent transform of the node.</param>
    /// <param name="index">Index of the node.</param>
    /// <returns>The created node game object.</returns>
    public GameObject CreateNode(NodeType type, Vector3 position, Transform parent, int index)
    {
        GameObject nodeObject = Object.Instantiate(nodePrefab, position, Quaternion.identity, parent);
        nodeObject.GetComponent<NodeClass>().Initialize(type, index);
        return nodeObject;
    }
}

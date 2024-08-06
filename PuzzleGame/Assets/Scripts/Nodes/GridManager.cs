using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Manages the grid of nodes.
/// </summary>
public class GridManager : MonoBehaviour
{
    public GameObject nodePrefab;
    public RectTransform puzzleLayoutImage;
    public LineRenderer lineRendererPrefab;
    private Transform gridParent; // Parent transform for the grid nodes

    private NodeFactory nodeFactory;
    private LineRenderer layoutLineRenderer;

    void Awake()
    {
        if (nodePrefab == null)
        {
            Debug.LogError("Node prefab is not assigned in the GridManager.");
        }

        nodeFactory = new NodeFactory(nodePrefab);
    }

    /// <summary>
    /// Sets the grid parent transform dynamically.
    /// </summary>
    /// <param name="parent">The new grid parent transform.</param>
    public void SetGridParent(Transform parent)
    {
        gridParent = parent;
    }

    /// <summary>
    /// Generates the grid of nodes with specific configuration.
    /// </summary>
    public LevelConfiguration GenerateGrid(int level, int gridSize)
    {
        if (gridParent == null)
        {
            Debug.LogError("Grid parent is not assigned.");
            return null;
        }

        ClearGrid();

        ProceduralNodeGenerator.GenerateNodePositions(level, gridSize, out List<Vector2Int> positions, out List<NodeType> types, out List<(int, int)> expectedConnections);

        RectTransform parentRect = gridParent.GetComponent<RectTransform>();
        Vector2 parentSize = parentRect.rect.size;
        Vector2 nodeSize = new Vector2(parentSize.x / gridSize, parentSize.y / gridSize); // Node size based on grid size

        List<Vector3> nodeWorldPositions = new List<Vector3>();

        for (int i = 0; i < positions.Count; i++)
        {
            Vector2Int pos = positions[i];
            Vector3 localPosition = new Vector3(
                (pos.x * nodeSize.x) - (parentSize.x / 2) + (nodeSize.x / 2),
                (pos.y * nodeSize.y) - (parentSize.y / 2) + (nodeSize.y / 2),
                0);

            GameObject nodeObject = nodeFactory.CreateNode(types[i], Vector3.zero, gridParent, i);
            nodeObject.transform.localPosition = localPosition;

            // Ensure the node has a BoxCollider2D component for click detection
            if (nodeObject.GetComponent<BoxCollider2D>() == null)
            {
                nodeObject.AddComponent<BoxCollider2D>();
            }

            nodeWorldPositions.Add(gridParent.TransformPoint(localPosition));
        }

        UpdatePuzzleLayoutImage(nodeWorldPositions);

        return new LevelConfiguration(gridSize, gridSize, positions, types, expectedConnections);
    }

    /// <summary>
    /// Clears the current grid.
    /// </summary>
    private void ClearGrid()
    {
        if (gridParent != null)
        {
            foreach (Transform child in gridParent)
            {
                Destroy(child.gameObject);
            }
        }

        if (layoutLineRenderer != null)
        {
            Destroy(layoutLineRenderer.gameObject);
        }
    }

    /// <summary>
    /// Updates the puzzle layout image based on node positions.
    /// </summary>
    private void UpdatePuzzleLayoutImage(List<Vector3> nodePositions)
    {
        if (lineRendererPrefab == null)
        {
            Debug.LogError("LineRenderer prefab is not assigned.");
            return;
        }

        layoutLineRenderer = Instantiate(lineRendererPrefab, puzzleLayoutImage.transform);

        if (layoutLineRenderer == null)
        {
            Debug.LogError("Failed to instantiate LineRenderer.");
            return;
        }

        layoutLineRenderer.positionCount = nodePositions.Count;
        for (int i = 0; i < nodePositions.Count; i++)
        {
            Vector3 localPosition = puzzleLayoutImage.InverseTransformPoint(nodePositions[i]);
            layoutLineRenderer.SetPosition(i, localPosition);
        }

        // Close the loop to form a shape
        layoutLineRenderer.loop = true;
    }

    public LineRenderer GetLayoutLineRenderer()
    {
        return layoutLineRenderer;
    }
}

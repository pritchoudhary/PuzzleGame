using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

/// <summary>
/// Manages the grid of nodes.
/// </summary>
public class GridManager : MonoBehaviour
{
    public int rows = 5;
    public int columns = 5;
    public GameObject nodePrefab;
    public Color[] nodeColors;

    private NodeClass[,] grid;
    private NodeFactory nodeFactory;

    void Awake()
    {
        nodeFactory = new NodeFactory(nodePrefab, nodeColors);
    }

    void Start()
    {
        GenerateGrid();
        InvokeRepeating("CheckPuzzleCompletion", 1.0f, 1.0f); // Check every second
    }

    /// <summary>
    /// Generates the grid of nodes.
    /// </summary>
    public void GenerateGrid()
    {
        ClearGrid();

        grid = new NodeClass[rows, columns];
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                Vector3 position = new Vector3(c, r, 0);
                NodeClass node = nodeFactory.CreateNode(position, transform);
                grid[r, c] = node;
            }
        }
    }

    /// <summary>
    /// Generates the grid of nodes with specific configuration.
    /// </summary>
    /// <param name="positions">List of node positions.</param>
    /// <param name="types">List of node types.</param>
    /// <param name="colors">List of node colors.</param>
    public void GenerateGrid(List<Vector2Int> positions, List<NodeType> types, List<Color> colors)
    {
        ClearGrid();

        grid = new NodeClass[rows, columns];
        for (int i = 0; i < positions.Count; i++)
        {
            Vector2Int pos = positions[i];
            Vector3 position = new Vector3(pos.x, pos.y, 0);
            GameObject nodeObject = Instantiate(nodePrefab, position, Quaternion.identity, transform);
            NodeClass node = nodeObject.GetComponent<NodeClass>();
            node.Initialize(types[i], colors[i]);
            grid[pos.x, pos.y] = node;
        }
    }

    /// <summary>
    /// Clears the current grid.
    /// </summary>
    private void ClearGrid()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void CheckPuzzleCompletion()
    {
        if (PuzzleChecker.IsPuzzleComplete(grid))
        {
            Debug.Log("Puzzle Completed!");
            // Implement additional logic for when the puzzle is completed.
            //load the next level or display a success message.
        }
    }
}

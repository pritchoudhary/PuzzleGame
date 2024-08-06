using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generates node positions procedurally based on the level and grid size.
/// </summary>
public static class ProceduralNodeGenerator
{
    public static void GenerateNodePositions(int level, int gridSize, out List<Vector2Int> positions, out List<NodeType> types)
    {
        positions = new List<Vector2Int>();
        types = new List<NodeType>();

        
        positions.Add(new Vector2Int(0, 0));
        types.Add(NodeType.Corner);
        positions.Add(new Vector2Int(0, gridSize - 1));
        types.Add(NodeType.Corner);
        positions.Add(new Vector2Int(gridSize - 1, 0));
        types.Add(NodeType.Corner);
        positions.Add(new Vector2Int(gridSize - 1, gridSize - 1));
        types.Add(NodeType.Corner);

        for (int i = 1; i < gridSize - 1; i++)
        {
            positions.Add(new Vector2Int(i, 0));
            types.Add(NodeType.Straight);
            positions.Add(new Vector2Int(i, gridSize - 1));
            types.Add(NodeType.Straight);
            positions.Add(new Vector2Int(0, i));
            types.Add(NodeType.Straight);
            positions.Add(new Vector2Int(gridSize - 1, i));
            types.Add(NodeType.Straight);
        }

        // Add center node
        positions.Add(new Vector2Int(gridSize / 2, gridSize / 2));
        types.Add(NodeType.Cross);
    }
}

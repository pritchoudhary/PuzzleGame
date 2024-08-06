using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generates node positions procedurally based on the level and grid size.
/// </summary>
public static class ProceduralNodeGenerator
{
    public static void GenerateNodePositions(int level, int gridSize, out List<Vector2Int> positions, out List<NodeType> types, out List<(int, int)> expectedConnections)
    {
        positions = new List<Vector2Int>();
        types = new List<NodeType>();
        expectedConnections = new List<(int, int)>();

        // Level 1 - Closed Loop
        if (level == 1)
        {
            positions.Add(new Vector2Int(0, 0));
            types.Add(NodeType.Corner);
            positions.Add(new Vector2Int(0, gridSize - 1));
            types.Add(NodeType.Corner);
            positions.Add(new Vector2Int(gridSize - 1, 0));
            types.Add(NodeType.Corner);
            positions.Add(new Vector2Int(gridSize - 1, gridSize - 1));
            types.Add(NodeType.Corner);

            expectedConnections.Add((0, 1));
            expectedConnections.Add((1, 3));
            expectedConnections.Add((3, 2));
            expectedConnections.Add((2, 0));
        }
        // Level 2 - Closed Loop
        else if (level == 2)
        {
            positions.Add(new Vector2Int(0, 0));
            types.Add(NodeType.Corner);
            positions.Add(new Vector2Int(0, gridSize - 1));
            types.Add(NodeType.Corner);
            positions.Add(new Vector2Int(gridSize - 1, 0));
            types.Add(NodeType.Corner);
            positions.Add(new Vector2Int(gridSize - 1, gridSize - 1));
            types.Add(NodeType.Corner);
            positions.Add(new Vector2Int(gridSize / 2, 0));
            types.Add(NodeType.T);
            positions.Add(new Vector2Int(gridSize / 2, gridSize - 1));
            types.Add(NodeType.T);

            expectedConnections.Add((0, 1));
            expectedConnections.Add((1, 5));
            expectedConnections.Add((5, 3));
            expectedConnections.Add((3, 2));
            expectedConnections.Add((2, 4));
            expectedConnections.Add((4, 0));
        }
        // Level 3 - Closed Loop
        else if (level == 3)
        {
            positions.Add(new Vector2Int(0, 0));
            types.Add(NodeType.Corner);
            positions.Add(new Vector2Int(0, gridSize - 1));
            types.Add(NodeType.Corner);
            positions.Add(new Vector2Int(gridSize - 1, 0));
            types.Add(NodeType.Corner);
            positions.Add(new Vector2Int(gridSize - 1, gridSize - 1));
            types.Add(NodeType.Corner);
            positions.Add(new Vector2Int(gridSize / 2, 0));
            types.Add(NodeType.T);
            positions.Add(new Vector2Int(gridSize / 2, gridSize - 1));
            types.Add(NodeType.T);
            positions.Add(new Vector2Int(0, gridSize / 2));
            types.Add(NodeType.T);
            positions.Add(new Vector2Int(gridSize - 1, gridSize / 2));
            types.Add(NodeType.T);

            expectedConnections.Add((0, 1));
            expectedConnections.Add((1, 5));
            expectedConnections.Add((5, 4));
            expectedConnections.Add((4, 2));
            expectedConnections.Add((2, 3));
            expectedConnections.Add((3, 7));
            expectedConnections.Add((7, 6));
            expectedConnections.Add((6, 0));
        }
        // Level 4 - Closed Loop
        else if (level == 4)
        {
            positions.Add(new Vector2Int(0, 0));
            types.Add(NodeType.Corner);
            positions.Add(new Vector2Int(0, gridSize - 1));
            types.Add(NodeType.Corner);
            positions.Add(new Vector2Int(gridSize - 1, 0));
            types.Add(NodeType.Corner);
            positions.Add(new Vector2Int(gridSize - 1, gridSize - 1));
            types.Add(NodeType.Corner);
            positions.Add(new Vector2Int(gridSize / 2, 0));
            types.Add(NodeType.T);
            positions.Add(new Vector2Int(gridSize / 2, gridSize - 1));
            types.Add(NodeType.T);
            positions.Add(new Vector2Int(0, gridSize / 2));
            types.Add(NodeType.T);
            positions.Add(new Vector2Int(gridSize - 1, gridSize / 2));
            types.Add(NodeType.T);
            positions.Add(new Vector2Int(gridSize / 2, gridSize / 2));
            types.Add(NodeType.Cross);

            expectedConnections.Add((0, 1));
            expectedConnections.Add((1, 5));
            expectedConnections.Add((5, 4));
            expectedConnections.Add((4, 2));
            expectedConnections.Add((2, 3));
            expectedConnections.Add((3, 7));
            expectedConnections.Add((7, 0));
            expectedConnections.Add((2, 8));
            expectedConnections.Add((8, 9));
            expectedConnections.Add((9, 6));
            expectedConnections.Add((6, 0));
        }
    }
}

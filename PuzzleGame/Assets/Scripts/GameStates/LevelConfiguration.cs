using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Configuration for a specific level.
/// </summary>
public class LevelConfiguration
{
    public int Rows { get; }
    public int Columns { get; }
    public List<Vector2Int> NodePositions { get; }
    public List<NodeType> NodeTypes { get; }

    public LevelConfiguration(int rows, int columns, List<Vector2Int> nodePositions, List<NodeType> nodeTypes)
    {
        Rows = rows;
        Columns = columns;
        NodePositions = nodePositions;
        NodeTypes = nodeTypes;
    }
}

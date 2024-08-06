using System.Collections.Generic;
using UnityEngine;

public class LevelConfiguration
{
    public int Rows { get; }
    public int Columns { get; }
    public List<Vector2Int> Positions { get; }
    public List<NodeType> Types { get; }
    public List<(int, int)> ExpectedConnections { get; }

    public LevelConfiguration(int rows, int columns, List<Vector2Int> positions, List<NodeType> types, List<(int, int)> expectedConnections)
    {
        Rows = rows;
        Columns = columns;
        Positions = positions;
        Types = types;
        ExpectedConnections = expectedConnections;
    }
}

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Checks if the puzzle is completed.
/// </summary>
public class PuzzleChecker : MonoBehaviour
{
    public static bool IsPuzzleComplete(LevelConfiguration config, List<(int, int)> playerConnections)
    {
        foreach (var connection in config.ExpectedConnections)
        {
            if (!playerConnections.Contains(connection) && !playerConnections.Contains((connection.Item2, connection.Item1)))
            {
                return false;
            }
        }
        return true;
    }
}

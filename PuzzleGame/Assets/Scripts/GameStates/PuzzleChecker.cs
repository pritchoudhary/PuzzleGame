using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// Checks if the puzzle is completed.
/// </summary>
public class PuzzleChecker : MonoBehaviour
{
    public static bool IsPuzzleComplete(NodeClass[,] grid)
    {
        foreach (NodeClass node in grid)
        {
            if (!node.GetComponent<INodeConnectionChecker>().IsConnected())
            {
                return false;
            }
        }
        return true;
    }
}

using System.Collections.Generic;
using UnityEngine;

public class LineDrawingManager : MonoBehaviour
{
    public GameObject linePrefab; // Prefab for the line renderer
    public ParticleSystem correctConnectionParticle; // Particle effect for correct connection
    public ParticleSystem incorrectConnectionParticle; // Particle effect for incorrect connection
    public ParticleSystem puzzleCompleteParticle; // Particle effect for puzzle completion
    private LineRenderer currentLine; // Current line being drawn
    private List<Vector3> linePositions = new List<Vector3>(); // List of positions in the current line
    private Transform currentGridParent; // Current grid parent transform
    private LevelConfiguration currentLevelConfig; // Current level configuration
    private HashSet<(int, int)> drawnConnections = new HashSet<(int, int)>(); // Set of drawn connections

    /// <summary>
    /// Sets the grid parent and retrieves the current level configuration.
    /// </summary>
    /// <param name="gridParent">The transform of the grid parent.</param>
    public void SetGridParent(Transform gridParent)
    {
        currentGridParent = gridParent;
        currentLevelConfig = LevelManager.Instance.GetCurrentLevelConfiguration();
        Debug.Log("Grid Parent Set: " + currentGridParent.name);
    }

    /// <summary>
    /// Starts drawing a new line from the specified start point.
    /// </summary>
    /// <param name="startPoint">The transform of the start point.</param>
    public void StartDrawingLine(Transform startPoint)
    {
        if (currentGridParent == null)
        {
            Debug.LogError("Grid Parent not set.");
            return;
        }

        Vector3 startPosition = currentGridParent.InverseTransformPoint(startPoint.position);

        if (currentLine == null)
        {
            GameObject lineObject = Instantiate(linePrefab, currentGridParent);
            currentLine = lineObject.GetComponent<LineRenderer>();
            currentLine.positionCount = 2;
            currentLine.SetPosition(0, startPosition);
            currentLine.SetPosition(1, startPosition);
            linePositions.Clear();
            linePositions.Add(startPosition);
        }
        else
        {
            currentLine.positionCount++;
            currentLine.SetPosition(currentLine.positionCount - 1, startPosition);
            linePositions.Add(startPosition);
            CheckConnection();
            currentLine = null; // Reset for the next line
        }
    }

    /// <summary>
    /// Updates the position of the current line to follow the mouse or touch input.
    /// </summary>
    /// <param name="newPosition">The new position to update the line to.</param>
    public void UpdateCurrentLine(Vector3 newPosition)
    {
        if (currentLine != null)
        {
            Vector3 position = currentGridParent.InverseTransformPoint(newPosition);
            currentLine.SetPosition(currentLine.positionCount - 1, position);
        }
    }

    /// <summary>
    /// Clears all lines from the grid parent.
    /// </summary>
    public void ClearLines()
    {
        foreach (Transform child in currentGridParent)
        {
            if (child.CompareTag("Line"))
            {
                Destroy(child.gameObject);
            }
        }
        currentLine = null;
        linePositions.Clear();
        drawnConnections.Clear();
    }

    void Update()
    {
        if (currentLine != null)
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            worldPosition.z = 0; // Set z position to 0 for 2D
            UpdateCurrentLine(worldPosition);
        }
    }

    /// <summary>
    /// Checks if the connection made is correct and plays the appropriate particle effect.
    /// </summary>
    private void CheckConnection()
    {
        if (linePositions.Count < 2) return;

        int startIndex = linePositions.Count - 2;
        int endIndex = linePositions.Count - 1;

        int startNodeIndex = FindClosestNodeIndex(linePositions[startIndex], currentLevelConfig.Positions);
        int endNodeIndex = FindClosestNodeIndex(linePositions[endIndex], currentLevelConfig.Positions);

        if (currentLevelConfig.ExpectedConnections.Contains((startNodeIndex, endNodeIndex)) ||
            currentLevelConfig.ExpectedConnections.Contains((endNodeIndex, startNodeIndex)))
        {
            drawnConnections.Add((startNodeIndex, endNodeIndex));
            drawnConnections.Add((endNodeIndex, startNodeIndex));
            Instantiate(correctConnectionParticle, currentGridParent.TransformPoint(linePositions[endIndex]), Quaternion.identity);
        }
        else
        {
            Instantiate(incorrectConnectionParticle, currentGridParent.TransformPoint(linePositions[endIndex]), Quaternion.identity);
        }

        if (IsPuzzleComplete())
        {
            Instantiate(puzzleCompleteParticle, currentGridParent.position, Quaternion.identity);
            Invoke("LoadNextLevel", 2.0f); // Wait for 2 seconds before loading the next level
        }
    }

    /// <summary>
    /// Checks if the puzzle is complete by comparing drawn lines to the expected connections.
    /// </summary>
    /// <returns>True if the puzzle is complete; otherwise, false.</returns>
    private bool IsPuzzleComplete()
    {
        foreach (var connection in currentLevelConfig.ExpectedConnections)
        {
            if (!drawnConnections.Contains(connection))
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Finds the closest node index to the specified position.
    /// </summary>
    /// <param name="position">The position to find the closest node to.</param>
    /// <param name="positions">The list of node positions.</param>
    /// <returns>The index of the closest node.</returns>
    private int FindClosestNodeIndex(Vector3 position, List<Vector2Int> positions)
    {
        float minDistance = float.MaxValue;
        int closestIndex = -1;

        for (int i = 0; i < positions.Count; i++)
        {
            Vector3 nodePosition = new Vector3(positions[i].x, positions[i].y, 0);
            float distance = Vector3.Distance(position, nodePosition);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestIndex = i;
            }
        }

        return closestIndex;
    }

    /// <summary>
    /// Loads the next level after a short delay.
    /// </summary>
    private void LoadNextLevel()
    {
        LevelManager.Instance.LoadNextLevel();
    }
}

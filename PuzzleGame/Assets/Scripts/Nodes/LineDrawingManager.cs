using System.Collections.Generic;
using UnityEngine;

public class LineDrawingManager : MonoBehaviour
{
    public GameObject linePrefab;
    private LineRenderer currentLine;
    private List<Vector3> linePositions = new List<Vector3>();
    private Transform currentGridParent;

    public void SetGridParent(Transform gridParent)
    {
        currentGridParent = gridParent;
        Debug.Log("Grid Parent Set: " + currentGridParent.name);
    }

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
            currentLine = null; // Reset for the next line
        }
    }

    public void UpdateCurrentLine(Vector3 newPosition)
    {
        if (currentLine != null)
        {
            Vector3 position = currentGridParent.InverseTransformPoint(newPosition);
            currentLine.SetPosition(currentLine.positionCount - 1, position);
        }
    }

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
}

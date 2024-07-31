using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a single node in the grid
/// Handles interaction and initalization
/// </summary>
public class NodeClass : MonoBehaviour
{
    public NodeType _nodeType;
    private INodeRotation nodeRotation;
    private INodeAppearance nodeAppearance;
    private INodeConnectionChecker connectionChecker;

    private void Awake()
    {
        nodeRotation = GetComponent<INodeRotation>();
        nodeAppearance = GetComponent<INodeAppearance>();
        connectionChecker = GetComponent<INodeConnectionChecker>();
    }

    /// <summary>
    /// Initializes the node with the specified type and color.
    /// </summary>
    /// <param name="type">Type of the node.</param>
    /// <param name="color">Color of the node.</param>
    public void Initialize(NodeType type, Color color)
    {
        _nodeType = type;
        nodeAppearance.SetColor(color);
    }

    private void Update()
    {
        // Handle touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                HandleInput(touch.position);
            }
        }

        // Handle mouse input for testing
        if (Input.GetMouseButtonDown(0))
        {
            HandleInput(Input.mousePosition);
        }
    }

    private void HandleInput(Vector3 inputPosition)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(inputPosition);
        Collider2D hitCollider = Physics2D.OverlapPoint(worldPosition);
        if (hitCollider != null && hitCollider.transform == transform)
        {
            nodeRotation.Rotate();
            connectionChecker.CheckConnections();
        }
    }
}

public enum NodeType
{
    Straight,
    Corner,
    T,
    Cross
}

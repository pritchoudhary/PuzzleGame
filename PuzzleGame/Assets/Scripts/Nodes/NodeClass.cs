using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TreeEditor.TreeEditorHelper;

/// <summary>
/// Represents a single node in the grid
/// Handles interaction and initialization
/// </summary>
public class NodeClass : MonoBehaviour
{
    public NodeType _nodeType;
    private INodeRotation nodeRotation;
    private INodeConnectionChecker connectionChecker;
    private Image imageComponent;
    private BoxCollider2D boxCollider;
    private LineDrawingManager lineDrawingManager;
    public int Index { get; private set; }

    private void Awake()
    {
        Debug.Log("NodeClass Awake called on " + gameObject.name);
        nodeRotation = GetComponent<INodeRotation>();
        connectionChecker = GetComponent<INodeConnectionChecker>();
        imageComponent = GetComponent<Image>();
        boxCollider = GetComponent<BoxCollider2D>();
        lineDrawingManager = FindObjectOfType<LineDrawingManager>();

        if (nodeRotation == null) Debug.LogError("INodeRotation component is missing from the Node object: " + gameObject.name);
        if (connectionChecker == null) Debug.LogError("INodeConnectionChecker component is missing from the Node object: " + gameObject.name);
        if (imageComponent == null) Debug.LogError("Image component is missing from the Node object: " + gameObject.name);
        if (boxCollider == null) Debug.LogError("BoxCollider2D component is missing from the Node object: " + gameObject.name);
        if (lineDrawingManager == null) Debug.LogError("LineDrawingManager is missing from the scene");
        if (boxCollider != null) AlignColliderWithRectTransform();
    }

    private void AlignColliderWithRectTransform()
    {
        if (boxCollider != null && imageComponent != null)
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            boxCollider.size = rectTransform.sizeDelta;
        }
    }

    private void Start()
    {
        Debug.Log("NodeClass Start called on " + gameObject.name);
    }

    /// <summary>
    /// Initializes the node with the specified type and index.
    /// </summary>
    /// <param name="type">Type of the node.</param>
    /// <param name="index">Index of the node.</param>
    public void Initialize(NodeType type, int index)
    {
        Debug.Log("NodeClass Initialize called on " + gameObject.name);
        _nodeType = type;
        Index = index;
        if (TryGetComponent<Image>(out imageComponent))
        {
            Debug.Log("Image component is initialized on " + gameObject.name);
            imageComponent.color = GetColorForNodeType(type);
        }
        else
        {
            Debug.LogError("Image component is not initialized on " + gameObject.name);
        }
    }

    private Color GetColorForNodeType(NodeType type)
    {
        return type switch
        {
            NodeType.Straight => Color.red,
            NodeType.Corner => Color.blue,
            NodeType.T => Color.green,
            NodeType.Cross => Color.yellow,
            _ => Color.white,
        };
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
        worldPosition.z = 0; // Set z position to 0 for 2D
        Collider2D hitCollider = Physics2D.OverlapPoint(worldPosition);
        Debug.Log($"World Position: {worldPosition}, Hit Collider: {hitCollider}");
        if (hitCollider != null && hitCollider.transform == transform)
        {
            Debug.Log("Node clicked: " + gameObject.name);
            lineDrawingManager.StartDrawingLine(transform);
            nodeRotation?.Rotate();
            connectionChecker?.CheckConnections();
        }
        else
        {
            Debug.Log("Clicked outside the node");
        }
    }

    public bool CheckConnections()
    {
        return connectionChecker.IsConnected();
    }
}
public enum NodeType
{
    Straight,
    Corner,
    T,
    Cross
}
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Checks the connections for a node to determine if it is correctly connected.
/// </summary>
public class NodeConnectionChecker : MonoBehaviour, INodeConnectionChecker
{
    public Transform topConnection;
    public Transform bottomConnection;
    public Transform leftConnection;
    public Transform rightConnection;

    private NodeClass node;
    private NodeParticleEffects particleEffects;
    private Dictionary<Transform, Vector2> connectionDirections;

    void Awake()
    {
        node = GetComponent<NodeClass>();
        particleEffects = FindObjectOfType<NodeParticleEffects>();

        connectionDirections = new Dictionary<Transform, Vector2>
        {
            { topConnection, Vector2.up },
            { bottomConnection, Vector2.down },
            { leftConnection, Vector2.left },
            { rightConnection, Vector2.right }
        };
    }

    /// <summary>
    /// Determines if the node is correctly connected to adjacent nodes.
    /// </summary>
    /// <returns>True if the node is correctly connected; otherwise, false.</returns>
    public bool IsConnected()
    {
        bool isConnected = false;

        switch (node._nodeType)
        {
            case NodeType.Straight:
                isConnected = CheckStraightConnection();
                break;
            case NodeType.Corner:
                isConnected = CheckCornerConnection();
                break;
            case NodeType.T:
                isConnected = CheckTConnection();
                break;
            case NodeType.Cross:
                isConnected = CheckCrossConnection();
                break;
        }

        return isConnected;
    }

    /// <summary>
    /// Checks and updates the connections, triggering particle effects based on the result.
    /// </summary>
    public void CheckConnections()
    {
        if (IsConnected())
        {
            particleEffects.PlayCorrectEffect(transform.position);
        }
        else
        {
            particleEffects.PlayIncorrectEffect(transform.position);
        }
    }

    private bool CheckStraightConnection()
    {
        return CheckPairConnection(topConnection, bottomConnection) ||
               CheckPairConnection(leftConnection, rightConnection);
    }

    private bool CheckCornerConnection()
    {
        return CheckPairConnection(topConnection, rightConnection) ||
               CheckPairConnection(rightConnection, bottomConnection) ||
               CheckPairConnection(bottomConnection, leftConnection) ||
               CheckPairConnection(leftConnection, topConnection);
    }

    private bool CheckTConnection()
    {
        return CheckTripleConnection(topConnection, leftConnection, rightConnection) ||
               CheckTripleConnection(leftConnection, topConnection, bottomConnection) ||
               CheckTripleConnection(bottomConnection, leftConnection, rightConnection) ||
               CheckTripleConnection(rightConnection, topConnection, bottomConnection);
    }

    private bool CheckCrossConnection()
    {
        return CheckQuadrupleConnection(topConnection, bottomConnection, leftConnection, rightConnection);
    }

    private bool CheckPairConnection(Transform pointA, Transform pointB)
    {
        return CheckConnection(pointA, connectionDirections[pointA]) &&
               CheckConnection(pointB, connectionDirections[pointB]);
    }

    private bool CheckTripleConnection(Transform pointA, Transform pointB, Transform pointC)
    {
        return CheckConnection(pointA, connectionDirections[pointA]) &&
               CheckConnection(pointB, connectionDirections[pointB]) &&
               CheckConnection(pointC, connectionDirections[pointC]);
    }

    private bool CheckQuadrupleConnection(Transform pointA, Transform pointB, Transform pointC, Transform pointD)
    {
        return CheckConnection(pointA, connectionDirections[pointA]) &&
               CheckConnection(pointB, connectionDirections[pointB]) &&
               CheckConnection(pointC, connectionDirections[pointC]) &&
               CheckConnection(pointD, connectionDirections[pointD]);
    }

    private bool CheckConnection(Transform connectionPoint, Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(connectionPoint.position, direction, 0.1f);
        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent<NodeClass>(out var otherNode))
            {
                // Determine the reverse direction for alignment check
                Vector2 reverseDirection = -direction;

                // Find the corresponding connection point on the other node
                Transform otherConnectionPoint = GetCorrespondingConnectionPoint(otherNode, reverseDirection);

                if (otherConnectionPoint != null)
                {
                    // Check if the connection points are properly aligned
                    return Vector2.Distance(otherConnectionPoint.position, connectionPoint.position) < 0.1f;
                }
            }
        }
        return false;
    }

    private Transform GetCorrespondingConnectionPoint(NodeClass otherNode, Vector2 reverseDirection)
    {
        foreach (var kvp in otherNode.GetComponent<NodeConnectionChecker>().connectionDirections)
        {
            if (kvp.Value == reverseDirection)
            {
                return kvp.Key;
            }
        }
        return null;
    }
}
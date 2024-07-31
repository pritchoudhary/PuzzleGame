using UnityEngine;

/// <summary>
/// Handles the rotation logic for a node.
/// </summary>
public class NodeRotation : MonoBehaviour, INodeRotation
{
    private int rotationState = 0; // 0, 1, 2, 3 for 0, 90, 180, 270 degrees
    private const int rotationAngle = 90;

    void Update()
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
            Rotate();
        }
    }

    public void Rotate()
    {
        rotationState = (rotationState + 1) % 4;
        transform.Rotate(0, 0, rotationAngle);
    }
}
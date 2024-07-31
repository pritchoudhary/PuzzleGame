using UnityEngine;

/// <summary>
/// Handles the appearance of a node, specifically its color.
/// </summary>
public class NodeAppearance : MonoBehaviour, INodeAppearance
{
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }
}
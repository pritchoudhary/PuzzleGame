using UnityEngine;
using UnityEngine.UI;

public class PuzzleLayoutUpdater : MonoBehaviour
{
    public Image puzzleLayoutImage;

    public void UpdatePuzzleLayout(LineRenderer layoutLineRenderer)
    {
        if (puzzleLayoutImage == null)
        {
            Debug.LogError("PuzzleLayoutImage is not assigned.");
            return;
        }

        if (layoutLineRenderer == null)
        {
            Debug.LogError("LayoutLineRenderer is null.");
            return;
        }

        // Assign the layout line renderer to the puzzle layout image.
        layoutLineRenderer.transform.SetParent(puzzleLayoutImage.transform, false);
    }
}

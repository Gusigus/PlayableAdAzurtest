using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SafeArea : MonoBehaviour
{
    private RectTransform panelRectTransform;
    private Rect currentSafeArea = new Rect(0, 0, 0, 0);

    void Awake()
    {
        panelRectTransform = GetComponent<RectTransform>();
        ApplySafeArea();
    }

    void Update()
    {
        // Playable ads often switch orientations. We check every frame 
        // if the safe area changed (e.g., the user rotated their phone).
        if (currentSafeArea != Screen.safeArea)
        {
            ApplySafeArea();
        }
    }

    void ApplySafeArea()
    {
        currentSafeArea = Screen.safeArea;

        // Convert the safe area pixels to anchor percentages
        Vector2 anchorMin = currentSafeArea.position;
        Vector2 anchorMax = currentSafeArea.position + currentSafeArea.size;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        // Apply the new anchors to the panel
        panelRectTransform.anchorMin = anchorMin;
        panelRectTransform.anchorMax = anchorMax;
    }
}
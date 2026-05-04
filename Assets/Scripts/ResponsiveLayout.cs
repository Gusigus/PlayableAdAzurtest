using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HorizontalLayoutGroup), typeof(VerticalLayoutGroup))]
public class ResponsiveLayout : MonoBehaviour
{
    private HorizontalLayoutGroup horizontalLayout;
    private VerticalLayoutGroup verticalLayout;

    void Awake()
    {
        horizontalLayout = GetComponent<HorizontalLayoutGroup>();
        verticalLayout = GetComponent<VerticalLayoutGroup>();
        
        // Ensure both are set to Control Child Size (Width/Height) and Force Expand (Width/Height) in the Inspector!
    }

    void Update()
    {
        // If the screen is wider than it is tall (Landscape)
        if (Screen.width > Screen.height)
        {
            horizontalLayout.enabled = true;
            verticalLayout.enabled = false;
        }
        // If the screen is taller than it is wide (Portrait)
        else
        {
            horizontalLayout.enabled = false;
            verticalLayout.enabled = true;
        }
    }
}
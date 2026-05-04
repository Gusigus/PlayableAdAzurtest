using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIBurstEffect : MonoBehaviour
{
    [Header("UI Elements")]
    public RectTransform ripple;
    public Image rippleImage;
    public RectTransform[] particles;
    public Image[] particleImages;

    [Header("Settings")]
    public float burstDuration = 0.5f;
    public float burstRadius = 100f; // How far the particles fly in pixels

    private Vector3[] particleStartPositions;
    private Vector3[] particleEndPositions;

    void Start()
    {
        // 1. Calculate the math for the circle burst ONCE at the start to save performance
        int count = particles.Length;
        particleStartPositions = new Vector3[count];
        particleEndPositions = new Vector3[count];

        for (int i = 0; i < count; i++)
        {
            // Set starting point to center
            particleStartPositions[i] = Vector3.zero;

            // Calculate the angle for this specific particle so they form a perfect circle
            float angle = i * Mathf.PI * 2f / count;
            
            // Calculate where it should end up based on the radius
            float x = Mathf.Cos(angle) * burstRadius;
            float y = Mathf.Sin(angle) * burstRadius;
            particleEndPositions[i] = new Vector3(x, y, 0);
            
            // Hide them by default
            SetAlpha(particleImages[i], 0f);
        }
        
        SetAlpha(rippleImage, 0f);
    }

    // Call this function when the player clicks the invisible hitbox!
    public void PlayBurst()
    {
        StartCoroutine(AnimateBurst());
    }

    IEnumerator AnimateBurst()
    {
        float timePassed = 0f;

        while (timePassed < burstDuration)
        {
            timePassed += Time.deltaTime;
            
            // Progress goes from 0.0 to 1.0
            float percent = timePassed / burstDuration; 
            
            // Use an "Ease Out" math formula so it explodes fast, then slows down
            float easeOut = 1f - (1f - percent) * (1f - percent);

            // 1. Animate the Ripple (Scales up, fades out)
            ripple.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * 2f, easeOut);
            SetAlpha(rippleImage, 1f - percent);

            // 2. Animate the Particles (Move outward, fade out)
            for (int i = 0; i < particles.Length; i++)
            {
                particles[i].localPosition = Vector3.Lerp(particleStartPositions[i], particleEndPositions[i], easeOut);
                
                // Add a little spin
                particles[i].localEulerAngles = new Vector3(0, 0, easeOut * 180f);
                
                SetAlpha(particleImages[i], 1f - percent);
            }

            // Wait until the next frame
            yield return null; 
        }
    }

    // Helper function to keep the code clean
    void SetAlpha(Image img, float alpha)
    {
        Color c = img.color;
        c.a = alpha;
        img.color = c;
    }
}
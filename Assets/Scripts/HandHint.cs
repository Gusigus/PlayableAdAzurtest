using UnityEngine;
using UnityEngine.UI;

public class HandHint : MonoBehaviour
{
    [Header("UI References")]
    public Image handImage;        
    public RectTransform handRect; 

    [Header("Timing Settings")]
    public float timeToWait = 3f;  
    public float appearSpeed = 2f; // How fast the "twang" happens

    [Header("Twang Settings (The Juice)")]
    public float startingRotation = 45f; // Starts tilted 45 degrees
    
    [Header("Pulse Settings (After appearing)")]
    public float pulseSpeed = 5f;
    public float pulseSize = 0.15f;

    private float idleTimer = 0f;
    private float appearProgress = 0f; // Tracks the 0 to 1 animation state
    private Vector3 baseScale;
    private Color handColor;

    void Start()
    {
        baseScale = handRect.localScale;
        
        // Start completely invisible and tiny
        handColor = handImage.color;
        handColor.a = 0f;
        handImage.color = handColor;
        handRect.localScale = Vector3.zero; 
    }

    void Update()
    {
        // 1. Listen for player interaction
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            idleTimer = 0f;
            appearProgress = 0f; // Reset the animation so it twangs again next time
        }
        else
        {
            idleTimer += Time.deltaTime;
        }

        // 2. Handle the Animation Logic
        if (idleTimer >= timeToWait)
        {
            // Progress goes from 0.0 to 1.0
            appearProgress += Time.deltaTime * appearSpeed;
            float clampedProgress = Mathf.Clamp01(appearProgress);

            // Fade Alpha smoothly
            handColor.a = clampedProgress;
            handImage.color = handColor;

            if (clampedProgress < 1f)
            {
                // --- PHASE 1: THE TWANG APPEARANCE --- //
                
                // Get the elastic math multiplier
                float elasticT = ElasticOut(clampedProgress);

                // 1. Twang the Scale (it will briefly go larger than baseScale, then settle)
                handRect.localScale = baseScale * elasticT;

                // 2. Twang the Rotation (Using LerpUnclamped allows the bounce effect)
                float currentRot = Mathf.LerpUnclamped(startingRotation, 0f, elasticT);
                handRect.localEulerAngles = new Vector3(0, 0, currentRot);
            }
            else
            {
                // --- PHASE 2: THE IDLE PULSE --- //
                
                // Ensure rotation is perfectly flat
                handRect.localEulerAngles = Vector3.zero;

                // Play the continuous pointing pulse
                float wave = (Mathf.Sin((appearProgress - 1f) * pulseSpeed) + 1f) / 2f; 
                float currentScale = 1f - (wave * pulseSize); 
                handRect.localScale = baseScale * currentScale;
            }
        }
        else
        {
            // --- WAITING / HIDING --- //
            if (handColor.a > 0f)
            {
                // Fade out extremely fast when touched
                handColor.a -= Time.deltaTime * (appearSpeed * 3f); 
                handColor.a = Mathf.Clamp01(handColor.a);
                handImage.color = handColor;

                // Smoothly shrink back to zero
                handRect.localScale = Vector3.Lerp(handRect.localScale, Vector3.zero, Time.deltaTime * 15f);
            }
        }
    }

    // --- The Secret Sauce: Standard Elastic Out Math --- //
    // This turns a boring linear 0-to-1 transition into a rubber-band spring!
    float ElasticOut(float t)
    {
        if (t == 0) return 0;
        if (t == 1) return 1;
        float p = 0.3f;
        return Mathf.Pow(2, -10 * t) * Mathf.Sin((t - p / 4f) * (2f * Mathf.PI) / p) + 1f;
    }
}
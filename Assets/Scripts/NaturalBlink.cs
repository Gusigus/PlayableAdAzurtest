using System.Collections;
using UnityEngine;

public class NaturalBlink : MonoBehaviour
{
    [Header("Eye References")]
    [Tooltip("Drag your 'Dif_Eyes' GameObject here")]
    public GameObject difEyesAsset;

    [Header("Blink Timing (Seconds)")]
    public float minTimeBetweenBlinks = 2.0f;
    public float maxTimeBetweenBlinks = 6.0f;
    public float blinkCloseDuration = 0.12f;

    [Header("Natural Randomness")]
    [Range(0f, 1f)]
    [Tooltip("Chance to do a rapid double-blink (0.15 = 15% chance)")]
    public float doubleBlinkChance = 0.15f;

    private void Start()
    {
        if (difEyesAsset != null)
        {
            // Ensure the blink overlay starts completely off (eyes open)
            difEyesAsset.SetActive(false);
            
            // Start the infinite blinking loop
            StartCoroutine(BlinkRoutine());
        }
        else
        {
            Debug.LogError("NaturalBlink: Dif_Eyes asset is not assigned in the Inspector!");
        }
    }

    private IEnumerator BlinkRoutine()
    {
        // This loop runs infinitely while the GameObject is active
        while (true)
        {
            // 1. Calculate a random wait time until the next blink
            float waitTime = Random.Range(minTimeBetweenBlinks, maxTimeBetweenBlinks);
            yield return new WaitForSeconds(waitTime);

            // 2. Perform a single blink
            yield return StartCoroutine(PerformBlink());

            // 3. Roll the dice for a natural double-blink
            if (Random.value <= doubleBlinkChance)
            {
                // A tiny fraction of a second pause between the double blinks
                yield return new WaitForSeconds(0.08f); 
                yield return StartCoroutine(PerformBlink());
            }
        }
    }

    private IEnumerator PerformBlink()
    {
        // Turn ON the Dif_Eyes to simulate the eyelid closing
        difEyesAsset.SetActive(true);
        
        // Wait for the duration of the blink
        yield return new WaitForSeconds(blinkCloseDuration);
        
        // Turn OFF the Dif_Eyes to open the eyes again
        difEyesAsset.SetActive(false);
    }
}
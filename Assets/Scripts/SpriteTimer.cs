using UnityEngine;
using UnityEngine.UI;

public class SpriteTimer : MonoBehaviour
{
    [Header("Your 0-9 Sprites")]
    [Tooltip("Drag your number sprites here IN ORDER: 0, 1, 2, 3... 9")]
    public Sprite[] numberSprites; // This array MUST have exactly 10 items

    [Header("UI Image References")]
    public Image minuteTens;
    public Image minuteOnes;
    public Image secondTens;
    public Image secondOnes;

    [Header("Timer Settings")]
    public float timeLimitInSeconds = 600f; // 600 seconds = 10:00 minutes
    
    private float currentTime;
    private bool isTimerRunning = false;

    void Start()
    {
        currentTime = timeLimitInSeconds;
        isTimerRunning = true;
        
        // Update the visual immediately on start
        UpdateTimerDisplay(); 
    }

    void Update()
    {
        if (isTimerRunning)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                currentTime = 0;
                isTimerRunning = false;
                TimeRanOut();
            }

            UpdateTimerDisplay();
        }
    }

    void UpdateTimerDisplay()
    {
        // 1. Convert the raw seconds into total minutes and remaining seconds
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        // 2. Chop the numbers into single digits using division and modulo math
        int minTensDigit = minutes / 10;
        int minOnesDigit = minutes % 10;
        
        int secTensDigit = seconds / 10;
        int secOnesDigit = seconds % 10;

        // 3. Look up the correct sprite from the array and assign it to the UI Images
        minuteTens.sprite = numberSprites[minTensDigit];
        minuteOnes.sprite = numberSprites[minOnesDigit];
        secondTens.sprite = numberSprites[secTensDigit];
        secondOnes.sprite = numberSprites[secOnesDigit];
    }

    void TimeRanOut()
    {
        Debug.Log("Timer hit 00:00! Show the fail screen or CTA button.");
    }
}

using UnityEngine;
using UnityEngine.UI;

public class SlicedTimer : MonoBehaviour
{
    [Header("UI Reference")]
    public Slider timerSlider; // Drag your Timer_Bar here!

    [Header("Timer Settings")]
    public float totalTime = 10f; // Seconds
    private float timeLeft;

    void Start()
    {
        timeLeft = totalTime;
    }

    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            
            // Slider values go from 0 to 1, just like fillAmount
            timerSlider.value = timeLeft / totalTime;
        }
        else
        {
            // Time is up! 
            timeLeft = 0;
            // Show your CTA here
        }
    }
}
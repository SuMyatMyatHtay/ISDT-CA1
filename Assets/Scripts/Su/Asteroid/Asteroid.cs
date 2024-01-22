using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
public class Asteroid : MonoBehaviour
{

    [Header("Asteroids")]
    public GameObject firstAsteroid;
    public GameObject secondAsteroid;
    public GameObject thirdAsteroid; 
    public GameObject fourthAsteroid;
    public GameObject fifthAsteroid; 

    private Rigidbody rb1;
    private Rigidbody rb2;
    private Rigidbody rb3;
    private Rigidbody rb4;
    private Rigidbody rb5;

    public TextMeshProUGUI uiText;

    private float[] timerDurations = { 10f, 150f, 100f, 80f, 60f }; // durations in seconds
    private int currentTimerIndex = 0;
    private float timer;

    void Start()
    {
        StartTimer();
        rb1 = firstAsteroid.GetComponent<Rigidbody>();
        rb2 = secondAsteroid.GetComponent<Rigidbody>();
        rb3 = thirdAsteroid.GetComponent<Rigidbody>();
        rb4 = fourthAsteroid.GetComponent<Rigidbody>();
        rb5 = fifthAsteroid.GetComponent<Rigidbody>();
    }

    void Update()
    {
        UpdateTimer();
    }

    void StartTimer()
    {
        timer = timerDurations[currentTimerIndex];
        StartCoroutine(Countdown());
    }

    void UpdateTimer()
    {
        // Display or use the timer value as needed
        // For example, you could display it on a UI text element
        uiText.text = FormatTime(timer);
    }
    IEnumerator Countdown()
    {
        while (timer > 0f)
        {
            yield return new WaitForSeconds(1f); // Wait for 1 second
            timer--;

            UpdateTimer(); // Update the timer display every second
        }

        // Log a message when each separate timer is up
        Debug.Log("Timer " + (currentTimerIndex + 1) + " is up");
        switch (currentTimerIndex + 1)
        {
            case 1: 
                rb1.useGravity = true;
                break;
                
            case 2: 
                rb2.useGravity = true;
                break;

            case 3:
                rb3.useGravity = true; 
                break;

            case 4:
                rb4.useGravity = true;
                break;

            case 5:
                rb5.useGravity = true;
                break;

            default:
                break; 
        }

        // Move to the next timer in the sequence
        currentTimerIndex++;

        // Check if there are more timers in the sequence
        if (currentTimerIndex < timerDurations.Length)
        {
            StartTimer(); // Start the next timer
        }
        else
        {
            Debug.Log("Sequence of timers is complete");
        }
    }

    // Optional: Format the timer value into a user-friendly string (e.g., MM:SS)
    string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
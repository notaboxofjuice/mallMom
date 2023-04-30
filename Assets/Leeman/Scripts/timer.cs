using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timer : MonoBehaviour
{
    [Tooltip("Time in seconds")]
    public float timeLeft = 120.0f; // time left in seconds
    public TextMeshProUGUI timerText; // text to display time
    private void Update() {
        timeLeft -= Time.deltaTime;
        TimerText();
        if (timeLeft < 1) {
            TimeOut();
        }
    }
    private void TimerText() {
        int minutesLeft = Mathf.FloorToInt(timeLeft / 60); // minutes left
        int secondsLeft = Mathf.FloorToInt(timeLeft % 60); // seconds left
        timerText.text = "Time Left: " + minutesLeft.ToString() + ":" + secondsLeft.ToString();
    }
    private void TimeOut() {
        pauseMenu.GetInstance().stillTime = false; // no more time
        pauseMenu.GetInstance().pauseText.text = "You failed!"; // game over text
        pauseMenu.GetInstance().ListUpdate("heldItem", "You ran out of time!"); // reuse list update function
        pauseMenu.GetInstance().Pause(false); // go to pause menu
    }
}
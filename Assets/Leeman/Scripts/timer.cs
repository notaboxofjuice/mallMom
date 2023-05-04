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
        string minutesString = minutesLeft.ToString(); // convert to string
        if (minutesLeft < 10) { // add a leading 0 if less than 10
            minutesString = "0" + minutesString;
        }
        int secondsLeft = Mathf.FloorToInt(timeLeft % 60); // seconds left
        string secondsString = secondsLeft.ToString(); // convert to string
        if (secondsLeft < 10) { // add a leading 0 if less than 10
            secondsString = "0" + secondsString;
        }
        timerText.text = "Time Left: " + minutesString + ":" + secondsString; // update text
    }
    private void TimeOut() {
        pauseMenu.GetInstance().stillTime = false; // no more time
        pauseMenu.GetInstance().pauseText.text = "You failed!"; // game over text
        pauseMenu.GetInstance().ListUpdate("heldItem", "You ran out of time!"); // reuse list update function
        pauseMenu.GetInstance().Pause(false); // go to pause menu
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pauseMenu : MonoBehaviour
{ // singleton class
    // VARIABLES
    public GameObject menuHere; // menu to show/hide
    private bool isPaused = false;
    private static pauseMenu instance;
    [Header("To Do List Texts")]
    public TextMeshProUGUI restockText;
    public TextMeshProUGUI spillText;
    public TextMeshProUGUI fetchText;
    public TextMeshProUGUI heldItemText;
    // AWAKE & INIT
    private void Awake() {
        if (instance != null) { // there should be no other instance
            Debug.LogWarning("Found more than one Pause Menu");
        }
        instance = this; // it's me i'm the instance
        menuHere.SetActive(false); // hide menu
    }
    // UPDATE
    private void Update() {
        if (Input.GetButtonDown("Cancel")) { // player presses cancel
            Pause(isPaused); // send pause bool to function
        }
    }
    // PUBLIC METHODS
    public static pauseMenu GetInstance() { // other script references me
        return instance; // it's this one here i am :)
    }
    public void Restart() { // restart button
        UnityEngine.SceneManagement.SceneManager.LoadScene(0); // load scene 0
        Pause(isPaused); // unpause
    }
    public void Quit() { // quit button
        Application.Quit(); // quit the game
    }
    public void SetSensitivity(float sens) { // set sensitivity
        GameObject.FindWithTag("Player").GetComponent<FirstPersonController>().mouseSensitivity = sens; // set sensitivity
    }
    public void ListUpdate(string taskType, string newText) {
        switch (taskType) { // update text based on task type
            case "restock":
                restockText.text = newText;
                break;
            case "spill":
                spillText.text = newText;
                break;
            case "fetch":
                fetchText.text = newText;
                break;
            case "heldItem":
                heldItemText.text = newText;
                break;
        }
    }
    // PRIVATE METHODS
    private void Pause(bool currentPause) { // pause the game
        isPaused = !currentPause; // toggle; if paused, unpause
        if (isPaused) { // if game is paused
            Time.timeScale = 0; // pause time
            Cursor.lockState = CursorLockMode.None; // unlock cursor
        } else { // if game is not paused
            Time.timeScale = 1; // unpause time
            Cursor.lockState = CursorLockMode.Locked; // lock cursor
        }
        GameObject.FindWithTag("Player").GetComponent<FirstPersonController>().enabled = !isPaused; // enable/disable player
        menuHere.SetActive(isPaused); // show/hide menu based on pause
    }
}
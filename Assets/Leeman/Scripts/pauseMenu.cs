using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class pauseMenu : MonoBehaviour
{ // singleton class
    // VARIABLES
    public GameObject menuHere; // menu to show/hide
    public bool stillTime = true; // is there still time?
    private bool isPaused = false;
    private static pauseMenu instance;
    public TextMeshProUGUI pauseText;
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
    private void Start() {
        Pause(true); // start the game unpaused
    }
    // UPDATE
    private void Update() {
        if (Input.GetButtonDown("Cancel") && stillTime) { // player presses cancel and there is still time
            Pause(isPaused); // send pause bool to function
        }
    }
    // PUBLIC METHODS
    public static pauseMenu GetInstance() { // other script references me
        return instance; // it's this one here i am :)
    }
    public void Restart() { // restart button
        Pause(true); // unpause
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // load currentScene
        Debug.Log("Reloaded Scene");
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
    public void Pause(bool currentPause) { // pause the game
        isPaused = !currentPause; // toggle; if paused, unpause
        if (isPaused) { // if game is paused
            Time.timeScale = 0; // pause time
            Debug.Log("Time Scale: " + Time.timeScale);
            Cursor.lockState = CursorLockMode.None; // unlock cursor
            Debug.Log("Cursor Lock State: " + Cursor.lockState);
        } else { // if game is not paused
            Time.timeScale = 1; // unpause time
            Debug.Log("Time Scale: " + Time.timeScale);
            Cursor.lockState = CursorLockMode.Locked; // lock cursor
            Debug.Log("Cursor Lock State: " + Cursor.lockState);
        }
        GameObject.FindWithTag("Player").GetComponent<FirstPersonController>().enabled = currentPause; // enable/disable player
        menuHere.SetActive(isPaused); // show/hide menu based on pause
    }
}
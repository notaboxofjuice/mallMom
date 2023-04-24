using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gameManager : MonoBehaviour
{
    // EXTERNAL VARS
    [Header("Escalator Stuff")]
    public GameObject tapeMop;
    public GameObject tapeStock;
    public GameObject tapeFetch;
    [Header("Public Vars")]
    [Tooltip("Is the player in a minigame?")]
    public bool occupied = false;
    [Header("Restock Minigame")]
    [Tooltip("Held box")]
    public GameObject currentBox;
    [Tooltip("Total shelves to restock")]
    public int shelfTotal; // Total shelves to restock
    [Tooltip("Restocked shelves")]
    public int shelfCurrent = 0; // Restocked shelves
    [Header("Fetch Minigame")]
    [Tooltip("Currently held fetch item")]
    public GameObject heldFetch;

    [Tooltip("Total items to fetch")]
    public int fetchTotal;
    [Tooltip("Count of fetched items")]
    public int fetchCount = 0;
    // INTERNAL VARS
    [Header("Private Vars")]
    private static gameManager instance;
    // INITIALIZATION & UPDATE
    private void Awake() {
        if (instance != null) { // there should be no other instance
            Debug.LogWarning("Found more than one Game Manager");
        }
        instance = this; // it's me i'm the instance
    }
    private void Start() {
        pauseMenu.GetInstance().ListUpdate("restock", "?????");
        pauseMenu.GetInstance().ListUpdate("fetch", "?????");
        pauseMenu.GetInstance().ListUpdate("heldItem", "?????");
    }
    // PUBLIC METHODS
    public static gameManager GetInstance() { // other script references me
        return instance; // it's this one here i am :)
    }
    public void AddShelf() { // triggered on restock
        shelfCurrent += 1; // increment counter
        RestockUI(); // update my UI
        if (shelfCurrent == shelfTotal) {
            Destroy(tapeStock); // destroy a piece of tape
            pauseMenu.GetInstance().ListUpdate("restock", "All shelves restocked!"); // remove restock from pause menu
            occupied = false; // player no longer in minigame
        }
    }
    public void AddFetch() { // triggered on successful fetch
        fetchCount += 1; // increment counter
        FetchUI();
        if (fetchCount == fetchTotal) { 
            Destroy(tapeFetch); // destroy a piece of tape
            pauseMenu.GetInstance().ListUpdate("fetch", "All customers helped!"); // remove fetch from pause menu
            occupied = false;
        }
        pauseMenu.GetInstance().ListUpdate("heldItem", ""); // remove held item from pause menu
    }
    public void RestockUI() {
        pauseMenu.GetInstance().ListUpdate("restock", "Restock " + (shelfTotal - shelfCurrent).ToString() + " shelves");
    }
    public void FetchUI() {
        pauseMenu.GetInstance().ListUpdate("fetch", "Fetch " + (fetchTotal - fetchCount).ToString() + " items");
        pauseMenu.GetInstance().ListUpdate("heldItem", "Holding: " + heldFetch.name.ToString());
    }
}
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
    [SerializeField] TextMeshProUGUI shelfText;
    [Header("Fetch Minigame")]
    [Tooltip("Currently held fetch item")]
    public GameObject heldFetch;

    [Tooltip("Total items to fetch")]
    public int fetchTotal;
    [Tooltip("Count of fetched items")]
    public int fetchCount = 0;
    [SerializeField] TextMeshProUGUI fetchText;
    [SerializeField] TextMeshProUGUI heldText;
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
        shelfText.text = ""; // empty shelf text
        fetchText.text = ""; // empty fetch text
        heldText.text = ""; // empty held item
    }
    // PUBLIC METHODS
    public static gameManager GetInstance() { // other script references me
        return instance; // it's this one here i am :)
    }
    public void AddShelf() { // triggered on restock
        shelfCurrent += 1; // increment counter
        if (shelfCurrent == shelfTotal) {
            Destroy(tapeStock); // destroy a piece of tape
            Destroy(shelfText.transform.parent.gameObject); // destory restockUI
            occupied = false; // player no longer in minigame
        }
        RestockUI(); // update my UI
    }
    public void AddFetch() { // triggered on successful fetch
        fetchCount += 1; // increment counter
        if (fetchCount == fetchTotal) { 
            Destroy(tapeFetch); // destroy a piece of tape
            Destroy(fetchText.transform.parent.gameObject); // destroy fetchUI
            occupied = false;
        }
        heldText.text = "";
    }
    public void RestockUI() {
        shelfText.text = "Empty Shelves Remaining: " + (shelfTotal - shelfCurrent).ToString();
    }
    public void FetchUI() {
        fetchText.text = "Items to Fetch: " + (fetchTotal - fetchCount).ToString();
        heldText.text = "Picked up: " + heldFetch.name.ToString();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class mopManager : MonoBehaviour
{
    private GameObject[] spills;
    int spillCount;
    [Tooltip("Drag in the spillText TMP object here")]
    [SerializeField] TextMeshProUGUI spillText;
    private void Awake() { // initiate list of spills
        // initiate list of spills
        spills = GameObject.FindGameObjectsWithTag("Spill");
        spillCount = spills.Length;
        Debug.Log("Spills: " + spillCount);
    }
    private void Start() {
        spillText.enabled = false; // disable the textbox
        updateSpills();
    }
    public void mopPickup()
    { // when the player picks up the mop
        Debug.Log("Picked up mop");
        spillText.enabled = true; // bring the text back
        // teleport mop to player
        transform.position = GameObject.FindWithTag("Player").transform.position;
        // set player as parent transform
        transform.parent = GameObject.FindWithTag("Player").transform;
    }
    public void cleanSpill()
    {
        Debug.Log("Cleaning up spill");
        spillCount -= 1; // decrement spill count
        updateSpills();
    }
    private void updateSpills()
    { // updates spillText
        if (spillCount > 0)
        { // if there are still spills remaining
            spillText.text = ("Spills remaining: " + spillCount); // update the text
        }
        else
        { // if all spills cleaned
            spillText.enabled = false; // hide text
            Destroy(this.gameObject); // destroy mop
            // destroy piece of tape blocking the escalator (comes later)
        }
    }
}
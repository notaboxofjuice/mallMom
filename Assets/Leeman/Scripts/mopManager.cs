using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class mopManager : MonoBehaviour
{
    private GameObject[] spills;
    int spillCount;
    public GameObject mopNPC;
    private void Awake() { // initiate list of spills
        // initiate list of spills
        spills = GameObject.FindGameObjectsWithTag("Spill");
        spillCount = spills.Length;
        Debug.Log("Spills: " + spillCount);
    }
    private void Start() {
        pauseMenu.GetInstance().ListUpdate("spill", "?????"); // empty spills on todo list

    }
    public void mopPickup()
    { // when the player picks up the mop
        Debug.Log("Picked up mop");
        updateSpills();
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
            pauseMenu.GetInstance().ListUpdate("spill", "Clean up spills: " + spillCount); // update the text
        }
        else
        { // if all spills cleaned
            pauseMenu.GetInstance().ListUpdate("spill", "All spills cleaned!"); // remove spills from to-do
            mopNPC.GetComponent<audioHandler>().SetClips("positive"); // set audio clips to positive
            Destroy(this.gameObject); // destroy mop
            Destroy(gameManager.GetInstance().tapeMop); // destroy piece of tape blocking the escalator
            gameManager.GetInstance().occupied = false; // player is no longer occupied
        }
    }
}
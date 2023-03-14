using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDetection : MonoBehaviour
{ // assign to NPC prefab parent object, which has a proximity detection collider
    private void OnTriggerEnter(Collider other) { // when something enters the collider
        if (other.CompareTag("Player")) { // and that thing is a player
            GetComponentInChildren<dialogueTrigger>().playerInRange = true; // update bool in dialogueTrigger.cs
        }
    }
    private void OnTriggerExit(Collider other) { // and when collision ends
        if (other.CompareTag("Player")) { // and the player is gone
            GetComponentInChildren<dialogueTrigger>().playerInRange = false; // they are no longer in range
        }
    }
}
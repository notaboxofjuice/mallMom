using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueTrigger : MonoBehaviour
{ // to be assigned to bodyHere obj of NPC prefab
    /// VARIABLES
    private Transform player; // finding the player
    public bool playerInRange = false; // track if player close enough (updated by playerDetection.cs)
    public bool playerInteracted = false; // track if player interacted (updated by interactionHandler.cs)
    [Header("Ink JSON")] // using Ink for handling dialogue text
    public TextAsset inkJSON; // put the NPC's ink json file here in inspector
    /// METHODS
    void Start(){
        player = GameObject.FindWithTag("Player").transform; // find player transform
    }
    private void LateUpdate()
    {
        if (playerInRange) { // if the player is close
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z)); // look at player
            Debug.Log("Looking"); // print to console thx babe
            if (playerInteracted) { // AND they have interacted
                dialogueManager.GetInstance().EnterDialogueMode(inkJSON); // enter dialogue mode with current ink file
                playerInteracted = false; // reset interact bool
            } // passes inkJSON to dialogue manager instance
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueTrigger : MonoBehaviour
{ // to be assigned to bodyHere obj of NPC prefab
    /// VARIABLES
    private Transform player; // finding the player
    private Vector3 targetPos; // target position
    public bool playerInRange; // track if player close enough (updated by playerDetection.cs)
    public bool playerInteracted; // track if player interacted (updated by interactionHandler.cs)
    [Header("Ink JSON")] // using Ink for handling dialogue text
    [SerializeField] private TextAsset inkJSON; // put the NPC's ink json file here in inspector
    /// METHODS
    void Awake(){
        player = GameObject.FindWithTag("Player").transform; // find player transform
        playerInRange = playerInteracted = false; // player is not close or interacted
    }
    private void Update()
    {
        targetPos = new Vector3(player.position.x, transform.position.y, player.position.z); // track playerPos
        //Debug.Log("Player in range? " + playerInRange); // tell me if player is close
        //Debug.Log("Player interacted? " + playerInteracted); // tell me if player has interacted
        if (playerInRange) { // if the player is close
            transform.LookAt(targetPos); // look at player
            Debug.Log("Looking"); // print to console thx babe
            if (playerInteracted) { // AND they have interacted
                dialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            } // passes inkJSON to dialogue manager instance
        }
        else { // player is not close
            GetComponent<Collider>().enabled = true; // re-enable collider
        }
        playerInteracted = false; // they haven't interacted
    }
}
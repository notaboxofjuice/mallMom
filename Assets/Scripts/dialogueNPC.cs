using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class dialogueNPC : MonoBehaviour
{
    /// VARIABLES
    private GameObject player; // finding the player
    private TextMeshProUGUI textBox; // textBox obj
    // METHODS
    void Start()
    {
        player = GameObject.FindWithTag("Player"); // find player
        textBox = player.GetComponentInChildren<TextMeshProUGUI>(); // find textBox
    }


    public void dialogueStart()
    {
        // Player locks on target NPC
        player.transform.LookAt(this.transform); // look at NPC
        player.GetComponent<FirstPersonController>().enabled = false; // disable ctrlr
        // UI elements appear
        Conversation();
        // Text prints to UI

        // Player interacts to advance to next dialogue

        // When dialogue is finished, resume player control
        //player.GetComponent<FirstPersonController>().enabled = true;
    }
    private void Conversation()
    {
        Debug.Log("Updating text...");
        textBox.text = "please god";
    }
}
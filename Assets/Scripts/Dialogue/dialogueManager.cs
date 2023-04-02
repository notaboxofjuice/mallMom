using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class dialogueManager : MonoBehaviour
{
    /// VARIABLES
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [Header("Public Variables")]
    [Tooltip("For handling NPC collider")]
    public GameObject npcBody; // need this for handling NPC collider
    private Story currentStory; // current Ink file to display
    public bool dialogueIsPlaying; // track if dialogue is playing
    /// METHODS
    private static dialogueManager instance; // it's a singleton class
    private void Awake() { // init within heirarchy
        if (instance != null) { // ensure there are no other instances
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this; // create an instance of this
    }
    public static dialogueManager GetInstance() { // instancing i guess
        return instance; // still not super sure how these classes work
    }
    private void Start() { // on frame zero
        dialogueIsPlaying = false; // no dialogue playing
        dialoguePanel.SetActive(false); // hide panel
    }
    private void Update() { // to traverse the logic of the Ink story
        if (!dialogueIsPlaying) { // if there's no dialogue
            return; // gtfo
        } // below, dialogueIsPlayer == true
        if (Input.GetButtonDown("Interact")) { // if player interacts
            ContinueStory();
        }
    }
    public void EnterDialogueMode(TextAsset inkJSON) { // called from dialogueTrigger.cs
        currentStory = new Story(inkJSON.text); // init story from inkJSON file passed in
        dialogueIsPlaying = true; // we're in dialogue mode
        dialoguePanel.SetActive(true); // activate the panel
        npcBody.GetComponent<Collider>().enabled = false; // disable to prevent story restart
        GameObject.FindWithTag("Player").GetComponent<FirstPersonController>().enabled = false; // disable ctrlr
        ContinueStory();
    }
    private void ExitDialogueMode() {
        dialogueIsPlaying = false; // not playing
        dialoguePanel.SetActive(false); // no panel
        dialogueText.text = ""; // empty string
        GameObject.FindWithTag("Player").GetComponent<FirstPersonController>().enabled = true; // enable ctrlr
        Invoke("EnableCollider", 1.0f); // enable collider after 1 second
    }
    private void ContinueStory() {
        if (currentStory.canContinue) { // make sure there's more dialogue to play
            dialogueText.text = currentStory.Continue();
        } else { // empty JSON file
            ExitDialogueMode();
        }
    }
    void EnableCollider() { // enables NPC body's collider and clears the object
        npcBody.GetComponent<Collider>().enabled = true;
        npcBody = null;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactionHandler : MonoBehaviour
{
    /// VARIABLES
    Ray ray; // ray
    RaycastHit hit; // store hit info
    bool canInteract = true; // bool if can interact
    dialogueNPC script;
    /// METHODS
    void Update()
    {
        ray = new Ray(transform.position, transform.forward); // get ray from pos & forward
        if (Physics.Raycast(ray, out hit) && Input.GetAxis("Interact") > 0 && canInteract)
        { // raycast hits && interact && canInteract
            Transform hitObject = hit.transform; // get hit object's transform
            Debug.Log(hitObject); // print to console
            if (hitObject.tag == "NPC")
            { // if it's an NPC
                Debug.Log("That's an NPC"); // lmk
                script = hitObject.GetComponent<dialogueNPC>(); // get the script
                script.dialogueStart(); // trigger dialogue function
            }
            else
            { // if nothin
                Debug.Log("nothin");
            }
        }
    }
}
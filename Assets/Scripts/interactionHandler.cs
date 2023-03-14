using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactionHandler : MonoBehaviour
{ // to be assigned to FPS controller prefab
    /// VARIABLES
    Ray ray; // ray
    RaycastHit hit; // store hit info
    /// METHODS
    void Update() {
        ray = new Ray(transform.position, transform.forward); // get ray from pos & forward
        if (Physics.Raycast(ray, out hit) && Input.GetButtonDown("Interact"))
        { // raycast hits && interact
            Transform hitObject = hit.transform; // get hit object's transform
            Debug.Log("That's " + hitObject); // print to console
            if (hitObject.tag == "NPC")
            { // if it's an NPC
                hitObject.GetComponent<dialogueTrigger>().playerInteracted = true;
            }
            else
            { // if nothin
                Debug.Log("nothin");
            }
        }
    }
}
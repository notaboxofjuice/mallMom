using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactionHandler : MonoBehaviour
{ // to be assigned to player camera
    /// VARIABLES
    Ray ray; // ray
    RaycastHit hit; // store hit info
    [SerializeField] float rayRange = 4f;
    [SerializeField] bool hasMop = false;
    /// METHODS
    void Update() {
        ray = new Ray(transform.position, transform.forward); // get ray from pos & forward
        if (Physics.Raycast(ray, out hit, rayRange) && Input.GetButtonDown("Interact"))
        { // raycast hits && interact
            Transform hitObject = hit.transform; // get hit object's transform
            Debug.Log("Interacted with " + hitObject.name); // print to console
            if (hitObject.tag == "NPC")
            { // if it's an NPC
                hitObject.GetComponent<dialogueTrigger>().playerInteracted = true;
            }
            else if (hitObject.tag == "Spill" && hasMop) // if spill and hasMop
            { // clean
                GameObject.FindWithTag("Mop").GetComponent<mopManager>().cleanSpill();
                Destroy(hitObject.gameObject); // destroy spill
            }
            else if (hitObject.tag == "Shelf" && gameManager.GetInstance().currentBox != null) // if shelf and holding box
            {
                hitObject.GetComponentInParent<restockManager>().tryRestock();
            }
            else if (!gameManager.GetInstance().occupied) { // player is unoccupied,
                if (hitObject.tag == "Mop" && !hasMop) { // if mop and not hasMop
                    hitObject.GetComponent<mopManager>().mopPickup();
                    gameManager.GetInstance().occupied = hasMop = true; // player occupied with mop
                }
                else if (hitObject.tag == "Box" && gameManager.GetInstance().currentBox == null) { // if box and current box is null
                    hitObject.GetComponentInParent<restockManager>().boxPickup();
                    gameManager.GetInstance().occupied = true; // player occupied with box
                }
            }
            else
            { // if nothin
                Debug.Log("Nothing happened.");
            }
        }
    }
}